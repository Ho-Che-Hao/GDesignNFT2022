using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using GDesign2022NFT.Controllers;
using GDesign2022NFT.ViewModel.PicturesVMs;
using GDesign2022NFT.Model;
using GDesign2022NFT.DataAccess;


namespace GDesign2022NFT.Test
{
    [TestClass]
    public class PicturesControllerTest
    {
        private PicturesController _controller;
        private string _seed;

        public PicturesControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<PicturesController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as PicturesListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(PicturesVM));

            PicturesVM vm = rv.Model as PicturesVM;
            Pictures v = new Pictures();
			
            v.ID = 6;
            v.Name = "dp15Erl1h";
            v.Status = GDesign2022NFT.Model.PicturesStatusEnum.Delete;
            v.Md5Code = "n2G1BDJ4JGQ2X5N2w2";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Pictures>().Find(v.ID);
				
                Assert.AreEqual(data.ID, 6);
                Assert.AreEqual(data.Name, "dp15Erl1h");
                Assert.AreEqual(data.Status, GDesign2022NFT.Model.PicturesStatusEnum.Delete);
                Assert.AreEqual(data.Md5Code, "n2G1BDJ4JGQ2X5N2w2");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            Pictures v = new Pictures();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ID = 6;
                v.Name = "dp15Erl1h";
                v.Status = GDesign2022NFT.Model.PicturesStatusEnum.Delete;
                v.Md5Code = "n2G1BDJ4JGQ2X5N2w2";
                context.Set<Pictures>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(PicturesVM));

            PicturesVM vm = rv.Model as PicturesVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new Pictures();
            v.ID = vm.Entity.ID;
       		
            v.Name = "BnWBWELAaXYaQVWLi";
            v.Status = GDesign2022NFT.Model.PicturesStatusEnum.Avaliable;
            v.Md5Code = "ZWuB";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ID", "");
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.Status", "");
            vm.FC.Add("Entity.Md5Code", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Pictures>().Find(v.ID);
 				
                Assert.AreEqual(data.Name, "BnWBWELAaXYaQVWLi");
                Assert.AreEqual(data.Status, GDesign2022NFT.Model.PicturesStatusEnum.Avaliable);
                Assert.AreEqual(data.Md5Code, "ZWuB");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            Pictures v = new Pictures();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ID = 6;
                v.Name = "dp15Erl1h";
                v.Status = GDesign2022NFT.Model.PicturesStatusEnum.Delete;
                v.Md5Code = "n2G1BDJ4JGQ2X5N2w2";
                context.Set<Pictures>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(PicturesVM));

            PicturesVM vm = rv.Model as PicturesVM;
            v = new Pictures();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Pictures>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            Pictures v = new Pictures();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ID = 6;
                v.Name = "dp15Erl1h";
                v.Status = GDesign2022NFT.Model.PicturesStatusEnum.Delete;
                v.Md5Code = "n2G1BDJ4JGQ2X5N2w2";
                context.Set<Pictures>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            Pictures v1 = new Pictures();
            Pictures v2 = new Pictures();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 6;
                v1.Name = "dp15Erl1h";
                v1.Status = GDesign2022NFT.Model.PicturesStatusEnum.Delete;
                v1.Md5Code = "n2G1BDJ4JGQ2X5N2w2";
                v2.ID = 93;
                v2.Name = "BnWBWELAaXYaQVWLi";
                v2.Status = GDesign2022NFT.Model.PicturesStatusEnum.Avaliable;
                v2.Md5Code = "ZWuB";
                context.Set<Pictures>().Add(v1);
                context.Set<Pictures>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(PicturesBatchVM));

            PicturesBatchVM vm = rv.Model as PicturesBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Pictures>().Find(v1.ID);
                var data2 = context.Set<Pictures>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            Pictures v1 = new Pictures();
            Pictures v2 = new Pictures();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 6;
                v1.Name = "dp15Erl1h";
                v1.Status = GDesign2022NFT.Model.PicturesStatusEnum.Delete;
                v1.Md5Code = "n2G1BDJ4JGQ2X5N2w2";
                v2.ID = 93;
                v2.Name = "BnWBWELAaXYaQVWLi";
                v2.Status = GDesign2022NFT.Model.PicturesStatusEnum.Avaliable;
                v2.Md5Code = "ZWuB";
                context.Set<Pictures>().Add(v1);
                context.Set<Pictures>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(PicturesBatchVM));

            PicturesBatchVM vm = rv.Model as PicturesBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Pictures>().Find(v1.ID);
                var data2 = context.Set<Pictures>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as PicturesListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }


    }
}
