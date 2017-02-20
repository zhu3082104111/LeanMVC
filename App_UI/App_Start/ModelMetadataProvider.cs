using System.Web.Mvc;
using Extensions;
using ModelMetadataExtensions;

[assembly: WebActivator.PreApplicationStartMethod(typeof(App_UI.App_Start.ModelMetadataProvider), "Start")]
namespace App_UI.App_Start
{
    public static class ModelMetadataProvider
    {
        public static void Start()
        {
            ModelMetadataProviders.Current = new ConventionalModelMetadataProvider(false, typeof(lang));
        }
    }
}