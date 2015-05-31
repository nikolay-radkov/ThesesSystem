namespace ThesesSystem.Web
{
    using System.Web.Mvc;

    public class ViewEnginesConfig
    {
        public static void RegisterViewEngines(ViewEngineCollection viewEngineCollection)
        {
            viewEngineCollection.Clear();
            viewEngineCollection.Add(new RazorViewEngine());
        }
    }
}
