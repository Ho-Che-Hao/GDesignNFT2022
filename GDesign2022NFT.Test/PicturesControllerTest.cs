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
			
            v.ID = 47;
            v.Name = "ldthqcpAMURO5";
            v.Md5Code = "BVtEq";
            v.PhotoId = AddFileAttachment();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Pictures>().Find(v.ID);
				
                Assert.AreEqual(data.ID, 47);
                Assert.AreEqual(data.Name, "ldthqcpAMURO5");
                Assert.AreEqual(data.Md5Code, "BVtEq");
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
       			
                v.ID = 47;
                v.Name = "ldthqcpAMURO5";
                v.Md5Code = "BVtEq";
                v.PhotoId = AddFileAttachment();
                context.Set<Pictures>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(PicturesVM));

            PicturesVM vm = rv.Model as PicturesVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new Pictures();
            v.ID = vm.Entity.ID;
       		
            v.Name = "8R1dP19Ltq4C";
            v.Md5Code = "GhCl";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ID", "");
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.Md5Code", "");
            vm.FC.Add("Entity.PhotoId", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Pictures>().Find(v.ID);
 				
                Assert.AreEqual(data.Name, "8R1dP19Ltq4C");
                Assert.AreEqual(data.Md5Code, "GhCl");
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
        		
                v.ID = 47;
                v.Name = "ldthqcpAMURO5";
                v.Md5Code = "BVtEq";
                v.PhotoId = AddFileAttachment();
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
                Assert.AreEqual(data.IsValid, false);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            Pictures v = new Pictures();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ID = 47;
                v.Name = "ldthqcpAMURO5";
                v.Md5Code = "BVtEq";
                v.PhotoId = AddFileAttachment();
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
				
                v1.ID = 47;
                v1.Name = "ldthqcpAMURO5";
                v1.Md5Code = "BVtEq";
                v1.PhotoId = AddFileAttachment();
                v2.ID = 79;
                v2.Name = "8R1dP19Ltq4C";
                v2.Md5Code = "GhCl";
                v2.PhotoId = v1.PhotoId; 
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
				
                v1.ID = 47;
                v1.Name = "ldthqcpAMURO5";
                v1.Md5Code = "BVtEq";
                v1.PhotoId = AddFileAttachment();
                v2.ID = 79;
                v2.Name = "8R1dP19Ltq4C";
                v2.Md5Code = "GhCl";
                v2.PhotoId = v1.PhotoId; 
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
                Assert.AreEqual(data1.IsValid, false);
            Assert.AreEqual(data2.IsValid, false);
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

        private Guid AddFileAttachment()
        {
            FileAttachment v = new FileAttachment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.FileName = "LGrymy5Tz";
                v.FileExt = "KpOC7DKB";
                v.Path = "R5sUClyVwm5QD1SxX";
                v.Length = 10;
                v.UploadTime = DateTime.Parse("2021-02-22 14:13:10");
                v.SaveMode = "kR8C";
                v.ExtraInfo = "TsH8jC5BMo";
                v.HandlerInfo = "FncDQ64l";
                context.Set<FileAttachment>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
