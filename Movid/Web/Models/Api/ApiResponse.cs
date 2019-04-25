using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Movid.App.Models.Api
{
    public class ApiResponse
    {
        private List<string> _errors;
        private List<string> _highFives;

        public ApiResponse(System.Web.Http.ModelBinding.ModelStateDictionary modelState)
        {
            var errors = new List<string>();
            foreach (var model in modelState.Values)
            {
                if (model.Errors.Any())
                {
                    errors.AddRange(model.Errors.Select(error => error.ErrorMessage));
                }
            }
        }

        public ApiResponse(string error)
        {
            Errors.Add(error);
        }

        public ApiResponse(string error = "", string success = "")
        {
            if( !string.IsNullOrWhiteSpace(error) && !string.IsNullOrWhiteSpace(success))
                throw new ArgumentException("Something errored and was successful?  What are you doing?");

            if( !string.IsNullOrWhiteSpace(error))
                Errors.Add(error);

            if (!string.IsNullOrWhiteSpace(success))
                HighFives.Add(success);
        }


        public ApiResponse(ModelStateDictionary modelState)
        {
            var errors = new List<string>();
            foreach (var model in modelState.Values)
            {
                if (model.Errors.Any())
                {
                    errors.AddRange(model.Errors.Select(error => error.ErrorMessage));
                }
            }
        }

        public List<string> HighFives
        {
            get { return _highFives ?? (_highFives = new List<string>()); }
            set { _highFives = value; }
        }

        public List<string> Errors
        {
            get { return _errors ?? (_errors = new List<string>()); }
            set { _errors = value; }
        }

        public string Warning { get; set; }
        public object PostedModel { get; set; }
        public object Model { get; set; }
        
        public bool Success {  get
        {
            if (!Errors.Any() && string.IsNullOrWhiteSpace(Warning))
            {
                return true;
            }

            return false;
        }}

        public string Html { get; set; }
    }
}