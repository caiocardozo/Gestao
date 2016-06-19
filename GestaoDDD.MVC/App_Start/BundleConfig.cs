using System.Web.Optimization;

namespace GestaoDDD.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //referencia do jquery apontada na mao, assim cada atualizacao teria de vir aqui trocar
            //agora ele pega sempre a ultima versao instalada
            //removi o arquivo .min, arquivo .min tanto em .js quanto .css nao pode estar aqui
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                       "~/Scripts/Adicionais/menufixo.js"));
            //arquivo de mapa.js estava atrapalhando o carregamento do jquery acima
            //pois bundles gera um arquivo so minificado
            bundles.Add(new ScriptBundle("~/bundles/mapa").Include(
                       "~/Scripts/Adicionais/mapa.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //bundles.Add(new StyleBundle("~/Content/css").IncludeDirectory(
            //              "~/Content", "*.css", true));

            //bundles.Add(new StyleBundle("~/bundles/images").IncludeDirectory(
            //              "~/Images", "*.png", true));


            BundleTable.EnableOptimizations = true;
        }
    }
}