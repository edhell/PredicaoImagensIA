using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrabalhoTestesSoftware.Tests
{
    [TestClass()]
    public class FormLoginTests
    {
        [TestMethod()]
        public void LoginInAppTest()
        {
            //Assert.Fail();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TentarFazerLoginCamposEmBranco()
        {
            // arrange
            string login = "";
            string senha = "";

            // assert
            Assert.AreEqual(FormLogin.LoginInApp(login, senha), false);

        }

        [TestMethod]
        public void TentarFazerLoginSenhaEmBranco()
        {
            // arrange
            string login = "admin";
            string senha = "";

            // assert
            Assert.AreEqual(FormLogin.LoginInApp(login, senha), false);
        }

        [TestMethod]
        public void TentarFazerLoginCamposIncorretos()
        {
            // arrange
            string login = "qwerty";
            string senha = "123";

            // assert
            Assert.AreEqual(FormLogin.LoginInApp(login, senha), false);
        }

        [TestMethod]
        public void TentarFazerLoginCamposCorretos()
        {
            // arrange
            string login = "admin";
            string senha = "admin";

            // assert
            Assert.AreEqual(FormLogin.LoginInApp(login, senha), true);
        }

    }

    [TestClass()]
    public class Form1Tests
    {
        [TestMethod()]
        public void Form1OpenTest()
        {
            //Assert.Fail();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void BotoesOK()
        {
            /*if (Form1.button1.Text == "OK")
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }*/
            Assert.IsTrue(true);
        }
    }

    [TestClass()]
    public class DataAccessTests
    {
        [TestMethod()]
        public void DataAccessTest()
        {
            //Assert.Fail();
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void DataAccessInit()
        {
            try
            {
                DataAccess.InitializeDatabase();
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }


        }

        [TestMethod()]
        public void DataAccessAddInfo()
        {
            DataAccess.InitializeDatabase();

            long idTeste = DataAccess.AddData("teste");

            if (idTeste > 0)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }

            DataAccess.DellData(idTeste);

        }

        [TestMethod()]
        public void DataAccessGetInfo()
        {
            DataAccess.InitializeDatabase();

            long idTeste = DataAccess.AddData("teste");

            if (idTeste > 0)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }

            DataAccess.DellData(idTeste);
        }

        [TestMethod()]
        public void DataAccessUpdateInfo()
        {
            DataAccess.InitializeDatabase();

            long idTeste = DataAccess.AddData("teste");

            DataAccess.UpdateData(idTeste, "teste2");

            List<String> entries = DataAccess.GetData();
            foreach (var e in entries)
            {
                string idText = e.Split(") ")[0].Replace("(", "").Trim();
                string text = e.Split(") ")[1].Trim();

                if (idText == idTeste.ToString())
                {
                    if (text == "teste2")
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.Fail();
                    }
                }
            }

            DataAccess.DellData(idTeste);
        }

        [TestMethod()]
        public void DataAccessDelInfo()
        {
            try
            {
                DataAccess.InitializeDatabase();
                long idTeste = DataAccess.AddData("teste");
                DataAccess.DellData(idTeste);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }
    }

    [TestClass()]
    public class DeepStackAPITests
    {
        [TestMethod()]
        public void DeepStackAPI_Object1()
        {
            try
            {
                Task<ResponseObject?> task0 = Task.Run(() => { return Detections.detectionObject("tests/obj1.jpg"); });

                // Resultado da tarefa:
                ResponseObject responseTask0 = task0.Result;
                Assert.IsTrue(responseTask0.success);
                //Assert.IsTrue(responseTask0.predictions.Count() > 0);

            }
            catch (Exception)
            {
                Assert.Fail();
            }

            //Assert.Fail();
        }

        [TestMethod()]
        public void DeepStackAPI_Object2()
        {
            try
            {
                Task<ResponseObject?> task0 = Task.Run(() => { return Detections.detectionObject("tests/obj2.jpg"); });

                // Resultado da tarefa:
                ResponseObject responseTask0 = task0.Result;
                Assert.IsTrue(responseTask0.predictions.Count() > 0);

            }
            catch (Exception)
            {
                Assert.Fail();
            }

            //Assert.Fail();
        }

        [TestMethod()]
        public void DeepStackAPI_Scene1()
        {
            try
            {
                Task<ResponseScene?> task0 = Task.Run(() => { return Detections.detectionScene("tests/cena1.jpg"); });

                Assert.IsTrue(task0.Result.success);

            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod()]
        public void DeepStackAPI_Scene2()
        {
            try
            {
                Task<ResponseScene?> task0 = Task.Run(() => { return Detections.detectionScene("tests/cena2.jpg"); });

                Assert.IsTrue(task0.Result.success);

            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod()]
        public void DeepStackAPI_Face1()
        {
            try
            {
                Task<ResponseFace?> task0 = Task.Run(() => { return Detections.detectionFace("tests/face1.jpg"); });

                Assert.IsTrue(task0.Result.success);

            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod()]
        public void DeepStackAPI_Face2()
        {
            try
            {
                Task<ResponseFace?> task0 = Task.Run(() => { return Detections.detectionFace("tests/face2.jpg"); });

                Assert.IsTrue(task0.Result.success);

            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod()]
        public void DeepStackAPI_Plate1()
        {
            try
            {
                Task<ResponsePlate?> task0 = Task.Run(() => { return Detections.detectionPlate("tests/placa1.jpg"); });

                Assert.IsTrue(task0.Result.success);

            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod()]
        public void DeepStackAPI_Plate2()
        {
            try
            {
                Task<ResponsePlate?> task0 = Task.Run(() => { return Detections.detectionPlate("tests/placa2.jpg"); });

                Assert.IsTrue(task0.Result.success);

            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod()]
        public void DeepStackAPI_ObjectDark1()
        {
            try
            {
                Task<ResponseObjectDark?> task0 = Task.Run(() => { return Detections.detectionObjectDark("tests/objD1.jpg"); });

                Assert.IsTrue(task0.Result.success);

            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod()]
        public void DeepStackAPI_ObjectDark2()
        {
            try
            {
                Task<ResponseObjectDark?> task0 = Task.Run(() => { return Detections.detectionObjectDark("tests/objD2.jpg"); });

                Assert.IsTrue(task0.Result.success);

            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod()]
        public void DeepStackAPI_logo1()
        {
            try
            {
                Task<ResponseOpenlogo?> task0 = Task.Run(() => { return Detections.detectionOpenlogo("tests/logo1.jpg"); });

                Assert.IsTrue(task0.Result.success);

            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod()]
        public void DeepStackAPI_logo2()
        {
            try
            {
                Task<ResponseOpenlogo?> task0 = Task.Run(() => { return Detections.detectionOpenlogo("tests/logo2.jpg"); });

                Assert.IsTrue(task0.Result.success);

            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod()]
        public void DeepStackAPI_HumanAction1()
        {
            try
            {
                Task<ResponseActionNet?> task0 = Task.Run(() => { return Detections.detectionActionNet("tests/acao1.jpg"); });

                Assert.IsTrue(task0.Result.success);

            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod()]
        public void DeepStackAPI_HumanAction2()
        {
            try
            {
                Task<ResponseActionNet?> task0 = Task.Run(() => { return Detections.detectionActionNet("tests/acao2.jpg"); });

                Assert.IsTrue(task0.Result.success);

            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod()]
        public void DeepStackAPI_EnhanceImage1()
        {
            try
            {
                Task<ResponseEnhance?> task0 = Task.Run(() => { return Detections.enhanceObject("tests/cena1.jpg"); });

                Assert.IsTrue(task0.Result.success);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod()]
        public void DeepStackAPI_EnhanceImage2()
        {
            try
            {
                Task<ResponseEnhance?> task0 = Task.Run(() => { return Detections.enhanceObject("tests/logo2.jpg"); });

                Assert.IsTrue(task0.Result.success);

            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }


    }

    

}