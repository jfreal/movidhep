using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movid.Shared.Infrastructure.Conventions;
using Movid.Web.Conventions;
using Movid.Web.Conventions.Validation;
using Movid.Web.Infrastructure.Conventions;

namespace Movid.Web.Infrastructure
{
    public class ConventionsBootstrapper
    {
        public static void Bootstrap()
        {
            ModelMetadataProviders.Current = new ConventionMetadataProvider();
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new ConventionModelValidatorProvider());

            ConventionModelValidatorProvider.Conventions.Add(new EmailDataTypeShouldBeValidEmail());
            ConventionModelValidatorProvider.Conventions.Add(new PhoneNumberDataTypeIsValidType());

            ConventionMetadataProvider.Conventions.Add(new KeyAsHiddenFieldConvention());
            ConventionMetadataProvider.Conventions.Add(new EnumUsesEnumTemplateHint());
            ConventionMetadataProvider.Conventions.Add(new ScaffoldTurnsOffShow());
            ConventionMetadataProvider.Conventions.Add(new NullDisplayTextIsEmptyStringForStringsConvention());
            ConventionMetadataProvider.Conventions.Add(new SpaceOutCamelCasePropertyNameAsDisplayName());
            ConventionMetadataProvider.Conventions.Add(new UnsupportedDescriptionAttribute());
            ConventionMetadataProvider.Conventions.Add(new ImpliedDescriptionFromTypeNameForViewModels());
            ConventionMetadataProvider.Conventions.Add(new BooleanFieldsArentImplicitlyRequired_MostOfTheTimeTheyDefaultToFalse());
            ConventionMetadataProvider.Conventions.Add(new UnsupportedReadOnlyAttribute());
            ConventionMetadataProvider.Conventions.Add(new RadioButtonOptionsConvention());
            ConventionMetadataProvider.Conventions.Add(new CheckBoxOptionsConvention());
            ConventionMetadataProvider.Conventions.Add(new StringLengthConvention());
            ConventionMetadataProvider.Conventions.Add(new NumericOnlyConvention());
            ConventionMetadataProvider.Conventions.Add(new MarkdownFieldConvention());
        }
    }
}