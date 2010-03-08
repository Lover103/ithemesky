using IThemeSky.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IThemeSky.Model;
using System;

namespace IThemeSky.DataAccessTest
{
    
    
    /// <summary>
    ///This is a test class for IThemeManageRepositoryTest and is intended
    ///to contain all IThemeManageRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IThemeManageRepositoryTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        internal virtual IThemeManageRepository CreateIThemeManageRepository()
        {
            // TODO: Instantiate an appropriate concrete class.
            IThemeManageRepository target = ThemeRepositoryFactory.Default.GetThemeManageRepository();
            return target;
        }
        /// <summary>
        ///A test for AddTheme
        ///</summary>
        [TestMethod()]
        public void AddThemeTest()
        {
            IThemeManageRepository target = CreateIThemeManageRepository(); // TODO: Initialize to an appropriate value
            Theme theme = new Theme()
            {
                AddTime = DateTime.Now,
                AuthorId = 007,
                BadComments = 1,
                CategoryId = 0,
                CheckerId = 007,
                CheckState = CheckStateOption.Waitting,
                CommendIndex = 1,
                Comments = 3,
                Description = "test",
                DisplayState = DisplayStateOption.Deleted,
                Downloads = 100,
                FileSize = 100,
                GoodComments = 2,
                LastMonthDownloads = 1,
                LastWeekDownloads = 10,
                ParentCategoryId = 0,
                Source = SourceOption.M,
                ThumbnailName = Guid.NewGuid().ToString(),
                Title = "test",
                UpdateTime = DateTime.Now,
                Views = 1000,
            };
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.AddTheme(theme);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MappingThemeTag
        ///</summary>
        [TestMethod()]
        public void MappingThemeTagTest()
        {
            IThemeManageRepository target = CreateIThemeManageRepository(); // TODO: Initialize to an appropriate value
            int themeId = 0; // TODO: Initialize to an appropriate value
            string tagName = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.MappingThemeTag(themeId, tagName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddThemeDownloadUrl
        ///</summary>
        [TestMethod()]
        public void AddThemeDownloadUrlTest()
        {
            IThemeManageRepository target = CreateIThemeManageRepository(); // TODO: Initialize to an appropriate value
            int themeId = 0; // TODO: Initialize to an appropriate value
            string url = string.Empty; // TODO: Initialize to an appropriate value
            bool isDefault = false; // TODO: Initialize to an appropriate value
            SourceOption source = new SourceOption(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.AddThemeDownloadUrl(themeId, url, isDefault, source);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
