ko.bindingHandlers.fadeVisible = {
    init: function (element, valueAccessor) {
        // Initially set the element to be instantly visible/hidden depending on the value
        var value = valueAccessor();
        $(element).toggle(ko.utils.unwrapObservable(value)); // Use "unwrapObservable" so we can handle values that may or may not be observable
    },
    update: function (element, valueAccessor) {
        // Whenever the value subsequently changes, slowly fade the element in or out
        var value = valueAccessor();

        //performance of this was really slow on mobile
        //changed to plain show/hide for now until I find another alternative
        ko.utils.unwrapObservable(value) ? $(element).show() : $(element).fadeOut(200);
    }
};

var ViewModel = function () {
    var self = this;

    self.showPlan = ko.observable(false);

    self.email = ko.observable();
    self.protocolName = ko.observable();
    self.greeting = ko.observable();

    self.selectedBody = ko.observable();
    self.selectedSubCategory = ko.observable();

    self.exercises = ko.observableArray([]);
    self.plan = ko.observableArray([]);
    self.library = [];

    self.planCount = ko.computed(function () {
        return self.plan().length;
    });

    self.messages = ko.observableArray([]);

    self.showVideo = function () {
        var videoEmbed = $('#video-embed').clone();

        $('iframe', videoEmbed).attr('src', 'https://player.vimeo.com/video/' + this.VideoId);
        $('.modal-title').text(this.Name);
        videoEmbed.show();

        $('#video-preview-modal .modal-body').html(videoEmbed);
        $('#video-preview-modal').modal('show');

        $('#video-preview-modal').on('hidden.bs.modal', function () {
            
            $('#video-preview-modal .modal-body').html('');
        });

    };

    self.transferToPlan = function () {
        mixpanel.track("ExerciseAdded", { exercise: this });
        var copy = {};
        jQuery.extend(copy, this);
        self.plan.push(copy);
    
        //var index = self.exercises.indexOf(this);
        //$.each(self.library, function (i, v) {
        //    if (v.Id === copy.Id) {
        //        var itemCopy = [];
        //        jQuery.extend(itemCopy, v);
        //        self.exercises()[index] = itemCopy;
        //        //console.log(v);
        //    }
        //});

        self.exercises.valueHasMutated();
        var name = this.Name;
        addTimedMessage({ heading: 'Success!', icon: 'icon-ok', message: name + ' has been added to the Program.' });
    };

    self.clearMessage= function () {
        self.messages.remove(this);
    };

    self.removeFromPlan = function () {
        self.plan.remove(this);
    };
};

var viewModel = new ViewModel();
ko.applyBindings(viewModel);

function addTimedMessage(message) {
    viewModel.messages.push(message);
    self.setTimeout(function () { viewModel.messages.shift(); }, 4000);
}

$(function () {
    $.getJSON('/library/json', function (data) {
        $.each(data, function (i, item) {
            viewModel.exercises.push(item);
            var copy = [];
            jQuery.extend(copy, item);

            viewModel.library.push(copy);
        });
    });

    function resetPlanPanel() {
        viewModel.plan([]);
        viewModel.email('');
        viewModel.program.greeting('');
    }

    $('#add-Program').click(function () {
        var data = {};
        data.exercises = viewModel.plan();
        data.email = viewModel.email();

        //mixpanel.track("ProgramSent", { 'plan': data.exercises, 'email': viewModel.email() });

        $.ajax({
            url: "/newProgram/",
            type: "POST",
            data: JSON.stringify(data),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (b) {
                resetPlanPanel();
                addTimedMessage({ heading: 'Program Sent!', icon: 'icon-ok', message: '' });
            }
        });
    });
    
    $('#new-protocol').click(function () {
        var data = {};
        data.exercises = viewModel.plan();
        data.protocolName = viewModel.protocolName();

        mixpanel.track("ProgramSent", { 'plan': data.exercises, 'email': viewModel.email() });

        $.ajax({
            url: "/Program-builder/protocol/new",
            type: "POST",
            data: JSON.stringify(data),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (b) {
                resetPlanPanel();
                addTimedMessage({ heading: 'Protocol Saved!', icon: 'icon-ok', message: '' });
            }
        });
    });


    $('#library-search').keyup(function (e) {
        if ($(this).val() == "") {
            var clone = jQuery.extend([], viewModel.library);
            viewModel.exercises(clone);
        }
    });

    
    $('#patient-email-input').typeahead({
        items: 8,
        minLength: 0,
        source: function (query, process) {
            
            $.getJSON('/patient/search/' + query, function (data) {
                
                process(data);
            });
        }
    });

    function getHashValue(key) {
        if (window.location.hash) {

            var keys = location.hash.match(new RegExp(key + '=([^&]*)'));

            if (keys != null)
                return keys[1];

            return "";
        }

        return "";
    }
    
    if (getHashValue("Program") != "")
        console.log(getHashValue("Program"));

    $('#library-search').typeahead({
        items: 20,
        minLength: 0,
        matcher: function (item) {

            var iM = item.replace(new RegExp(">", "g"), '');
            var qM = this.query.replace(new RegExp(">", "g"), '');

            return ~iM.toLowerCase().indexOf(qM.toLowerCase());
            //return true;

        },
        source: function (query, process) {

            var check = query.replace(/^\s+|\s+$/, '');
            if (check === "") {
                var clone = jQuery.extend([], viewModel.library);
                viewModel.exercises(clone);
                return;
            }

            var term = query.replace(new RegExp(">", "g"), '');
            $.getJSON('/library/search/' + term, function (data) {

                var terms = [];
                $.each(data.Typeaheads, function (i, v) {
                    terms.push(v);
                });

                var clone = jQuery.extend([], data.Exercises);
                viewModel.exercises(clone);
                process(terms);
            });
        },
        updater: function (item) {
            var term = item.replace(new RegExp(">", "g"), '');

            $.getJSON('/library/search/' + term, function (data) {
                var clone = jQuery.extend([], data.Exercises);

                viewModel.exercises(clone);
            });

            return item;
        }
    });
});