using IThemeSky.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IThemeSky.Model;
using System.Collections.Generic;

namespace IThemeSky.DataAccessTest
{
    
    
    /// <summary>
    ///This is a test class for IThemeViewRepositoryTest and is intended
    ///to contain all IThemeViewRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IThemeViewRepositoryTest
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


        /// <summary>
        ///A test for GetThemesByIds
        ///</summary>
        [TestMethod()]
        public void GetThemesByIdsTest()
        {
            IThemeViewRepository target = CreateIThemeViewRepository(); // TODO: Initialize to an appropriate value
            List<int> themeIds = new List<int>();
            themeIds.Add(1);
            List<SimpleThemeView> actual;
            actual = target.GetThemesByIds(themeIds);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetTheme
        ///</summary>
        [TestMethod()]
        public void GetThemeTest()
        {
            IThemeViewRepository target = CreateIThemeViewRepository(); 
            int themeId = 1; 
            FullThemeView actual;
            actual = target.GetTheme(themeId);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetNextThemeId
        ///</summary>
        [TestMethod()]
        public void GetNextThemeIdTest()
        {
            //IThemeViewRepository target = CreateIThemeViewRepository(); // TODO: Initialize to an appropriate value
            //int themeId = 2; // TODO: Initialize to an appropriate value
            //int expected = 3; // TODO: Initialize to an appropriate value
            //int actual;
            //actual = target.GetNextThemeId(themeId);
            //Assert.AreEqual(expected, actual);

            //themeId = 999999;
            //expected = 0; // TODO: Initialize to an appropriate value
            //actual = target.GetNextThemeId(themeId);
            //Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetFullThemesByFilter
        ///</summary>
        [TestMethod()]
        public void GetFullThemesByFilterTest()
        {
            IThemeViewRepository target = CreateIThemeViewRepository(); 
            ThemesFilter filter = target.Filter;
            ThemeSortOption sort = ThemeSortOption.New;
            int pageIndex = 1;
            int pageSize = 10;
            int recordCount = 0; 
            
            List<FullThemeView> actual;
            actual = target.GetFullThemesByFilter(filter, sort, pageIndex, pageSize, ref recordCount);
            Assert.IsNotNull(actual);
        }

        internal virtual IThemeViewRepository CreateIThemeViewRepository()
        {
            IThemeViewRepository target = ThemeRepositoryFactory.Default.GetThemeViewRepository();
            return target;
        }

        /// <summary>
        ///A test for GetThemesByFilter
        ///</summary>
        [TestMethod()]
        public void GetThemesByFilterTest()
        {
            IThemeViewRepository target = CreateIThemeViewRepository();
            ThemesFilter filter = target.Filter;
            ThemeSortOption sort = ThemeSortOption.New;
            int pageIndex = 1;
            int pageSize = 10; 
            int recordCount = 0;
            List<SimpleThemeView> actual;
            actual = target.GetThemesByFilter(filter, sort, pageIndex, pageSize, ref recordCount);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetThemesByMultiTags
        ///</summary>
        [TestMethod()]
        public void GetThemesByMultiTagsTest()
        {
            IThemeViewRepository target = CreateIThemeViewRepository(); // TODO: Initialize to an appropriate value
            List<List<string>> tags = new List<List<string>>();
            tags.Add(new List<string>() { "testtag", "aa" });
            tags.Add(new List<string>() { "fdsafsafs" });
            ThemesFilter filter = target.Filter;
            ThemeSortOption sort = ThemeSortOption.Popular;
            int pageIndex = 1;
            int pageSize = 10;
            int recordCount = 0;
            List<SimpleThemeView> actual;
            actual = target.GetThemesByMultiTags(tags, filter, sort, pageIndex, pageSize, ref recordCount);
            Assert.IsNotNull(actual);
        }
    }
}
