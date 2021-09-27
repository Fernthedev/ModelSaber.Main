using System;

namespace ModelSaber.Main.Data
{
    //Add API redirects here
    //use
    //@inject APIService api
    public class APIService
    {
        public string Return() => DateTime.Now.ToString();
    }
}