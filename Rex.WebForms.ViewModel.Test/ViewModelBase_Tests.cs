using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rex.Lib;
using System;

namespace Rex.WebForms.ViewModel.Test
{
    /// <summary>
    /// Test the ViewModelBase data pipeline to make sure what is fed in via the
    /// ILoader is what comes out from the Data property
    /// </summary>
    [TestClass]
    public class ViewModelBase_Tests
    {
        [TestMethod]
        public void Data()
        {
            ILoader loader = new DatabaseWithNoSchemasTestLoader(); //Only database level of metadata hierarchy matters for this test
            ViewModelBaseTestImplementation v = new ViewModelBaseTestImplementation(loader);
            Assert.IsNotNull(v.Data);
            Assert.AreEqual(loader.Load().Name, v.Data.Name); //Loader and viewmodel are in sync
            Assert.AreEqual(loader.Load().Id, v.Data.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Data_LoaderIsNull()
        {
            ViewModelBaseTestImplementation v = new ViewModelBaseTestImplementation(null);
            Database d = v.Data; //null loader cannot load anything and blows up
        }

        [TestMethod]
        public void Data_NullDatabaseTestLoader()
        {
            ILoader loader = new NullDatabaseTestLoader();
            ViewModelBaseTestImplementation v = new ViewModelBaseTestImplementation(loader);
            Assert.IsNull(v.Data);
        }       
    }

    /// <summary>
    /// Trivial implementation of the abstract class that will be tested
    /// </summary>
    public class ViewModelBaseTestImplementation : ViewModelBase
    {
        public ViewModelBaseTestImplementation(ILoader loader)
            : base(loader)
        {
        }
    }
}