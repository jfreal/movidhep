var movidApp = angular.module('movidApp', ['$strap.directives', 'angularPayments', 'ngRoute', 'angularFileUpload']);

Array.prototype.remove = function(value) {
    var idx = this.indexOf(value);
    if (idx != -1) {
        return this.splice(idx, 1); // The second parameter is the number of elements to remove.
    }
    return false;
};

movidApp.factory('movidHttpInterceptor', ['$q', '$rootScope',function ($q, $rootScope) {
    return {
        // optional method
        'request': function (config) {
            // do something on success
            //console.log('request');
            return config || $q.when(config);
        },

        // optional method
        'requestError': function (rejection) {
            // do something on error
            //console.log(rejection);
            return $q.reject(rejection);
        },

        // optional method
        'response': function (response) {
            // do something on success

            if (response.status === 200 && response.config.method === 'POST') {
                if (response.data.success) {
                    $rootScope.highFives.push(response.data.highFives[0]);
                }
                
                if (!response.data.success) {
                    $rootScope.errors.push(response.data.errors[0]);
                }
            }

            return response || $q.when(response);
        },

        // optional method
        'responseError': function (rejection) {
            // do something on error
            console.log(rejection);
            return $q.reject(rejection);
        }
    };
}]);

movidApp.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('movidHttpInterceptor');
}]);

movidApp.factory('programService', function($http) {
    return {
        sendRandomToLoggedInUser: function (clinicId) {
            $http.post('/api/program/sendRandom', {}).then(function(r) {
                return r.data;
            });
        }
    };
});

movidApp.factory('planService', function ($http) {
    return {
        getPlanOptions: function () {
            return $http.get('/api/plan/options/upgrade').then(function (r) {
                return r.data;
            });
        },
        getCurrent: function() {
            return $http.get('/api/plan/current').then(function (r) {
                return r.data;
            });
        },
        changePlan: function (postedModel) {
            return $http.post('/api/plan/change', postedModel).then(function (r) {

                if (!r.data.success) {
                    return {
                        'message': r.data.errors[0],
                        'success': false
                    };
                } else {
                    return {
                        'success': true,
                        'model': r.data.Model
                    };
                }

                return r.data;
            }, function (r) {
                return {
                    'message': r.data.message,
                    'success': false
                };
            });
        }
        
    };
});

  
function appCntrl($scope, $rootScope) {
    $rootScope.errors = [];
    $rootScope.highFives = [];

    $scope.cleaHighFives = function() {
        $rootScope.highFives = [];
    };
    
    $scope.clearErrors = function () {
        $rootScope.errors = [];
    };
}

function settingsCntrl($scope, programService) {
    $scope.sendRandomToLoggedInUser = function() {
        programService.sendRandomToLoggedInUser();
    };
}