using System;
using System.Collections.Specialized;
using Jelly.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jelly.Tests.UtilitiesTest
{
    [TestClass]
    public class UriUtilityTest
    {
        [TestMethod]
        public void BuildQueryStringTest()
        {
            NameValueCollection nac = new NameValueCollection();
            nac.Add("a", "1");
            nac.Add("b", "2");
            nac.Add("c", "3");

            string queryString = UriUtility.BuildQueryString(nac);
            Assert.AreEqual("?a=1&b=2&c=3", queryString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AppendQueryDelimiterTest() 
        {
            string emptyUrl = null;
            string url = "http://www.123.com/?";
            string url2 = "http://www.123.com/?a=b";
            string url3 = "http://www.123.com/?a=b&";

            string actualEmptyUrl = UriUtility.AppendQueryDelimiter(emptyUrl);
            string actualUrl = UriUtility.AppendQueryDelimiter(url);
            string actualUrl2 = UriUtility.AppendQueryDelimiter(url2);
            string actualUrl3 = UriUtility.AppendQueryDelimiter(url3);

            Assert.AreEqual("http://www.123.com/?", actualUrl);
            Assert.AreEqual("http://www.123.com/?a=b&", actualUrl2);
            Assert.AreEqual("http://www.123.com/?a=b&", actualUrl3);
        }
    }
}
