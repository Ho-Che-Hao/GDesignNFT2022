using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using GDesign2022NFT.Controllers;
using GDesign2022NFT.ViewModel.MultiplePicturesVMs;
using GDesign2022NFT.Model;
using GDesign2022NFT.DataAccess;


namespace GDesign2022NFT.Test
{
    [TestClass]
    public class MultiplePicturesControllerTest
    {
        private MultiplePicturesController _controller;
        private string _seed;

        public MultiplePicturesControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<MultiplePicturesController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as MultiplePicturesListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(MultiplePicturesVM));

            MultiplePicturesVM vm = rv.Model as MultiplePicturesVM;
            MultiplePictures v = new MultiplePictures();
			
            v.ID = 2;
            v.Md5Code = "0Rk";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<MultiplePictures>().Find(v.ID);
				
                Assert.AreEqual(data.ID, 2);
                Assert.AreEqual(data.Md5Code, "0Rk");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            MultiplePictures v = new MultiplePictures();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ID = 2;
                v.Md5Code = "0Rk";
                context.Set<MultiplePictures>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(MultiplePicturesVM));

            MultiplePicturesVM vm = rv.Model as MultiplePicturesVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new MultiplePictures();
            v.ID = vm.Entity.ID;
       		
            v.Md5Code = "2m";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ID", "");
            vm.FC.Add("Entity.Md5Code", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<MultiplePictures>().Find(v.ID);
 				
                Assert.AreEqual(data.Md5Code, "2m");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            MultiplePictures v = new MultiplePictures();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ID = 2;
                v.Md5Code = "0Rk";
                context.Set<MultiplePictures>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(MultiplePicturesVM));

            MultiplePicturesVM vm = rv.Model as MultiplePicturesVM;
            v = new MultiplePictures();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<MultiplePictures>().Find(v.ID);
                Assert.AreEqual(data.IsValid, false);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            MultiplePictures v = new MultiplePictures();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ID = 2;
                v.Md5Code = "0Rk";
                context.Set<MultiplePictures>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            MultiplePictures v1 = new MultiplePictures();
            MultiplePictures v2 = new MultiplePictures();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 2;
                v1.Md5Code = "0Rk";
                v2.ID = 76;
                v2.Md5Code = "2m";
                context.Set<MultiplePictures>().Add(v1);
                context.Set<MultiplePictures>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(MultiplePicturesBatchVM));

            MultiplePicturesBatchVM vm = rv.Model as MultiplePicturesBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<MultiplePictures>().Find(v1.ID);
                var data2 = context.Set<MultiplePictures>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            MultiplePictures v1 = new MultiplePictures();
            MultiplePictures v2 = new MultiplePictures();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 2;
                v1.Md5Code = "0Rk";
                v2.ID = 76;
                v2.Md5Code = "2m";
                context.Set<MultiplePictures>().Add(v1);
                context.Set<MultiplePictures>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(MultiplePicturesBatchVM));

            MultiplePicturesBatchVM vm = rv.Model as MultiplePicturesBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<MultiplePictures>().Find(v1.ID);
                var data2 = context.Set<MultiplePictures>().Find(v2.ID);
                Assert.AreEqual(data1.IsValid, false);
            Assert.AreEqual(data2.IsValid, false);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as MultiplePicturesListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }


    }
}
