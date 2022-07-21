using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RecruitmentAssignmentSente.Tests
{
    [TestClass()]
    public class DataManagerTests
    {
        XSD.Structure GetTemplateStructure()
        {
            return new XSD.Structure
            {
                Employees = new XSD.Employee[]
                {
                    new XSD.Employee
                    {
                        id = 1,
                        Employees = new XSD.Employee[]
                        {
                            new XSD.Employee
                            {
                                id = 2,
                                Employees = null
                            },
                            new XSD.Employee
                            {
                                id = 3,
                                Employees = new XSD.Employee[]
                                {
                                    new XSD.Employee
                                    {
                                        id = 4,
                                        Employees = null
                                    }
                                }
                            },
                        }
                    }
                }
            };
        }

        XSD.Transfers GetTemplateTransfers()
        {
            return new XSD.Transfers
            {
                Transfer = new XSD.Transfer[]
                {
                    new XSD.Transfer
                    {
                        From = 2,
                        Amount = 100,
                    },
                    new XSD.Transfer
                    {
                        From = 3,
                        Amount = 50,
                    },
                    new XSD.Transfer
                    {
                        From = 4,
                        Amount = 100,
                    },
                    new XSD.Transfer
                    {
                        From = 4,
                        Amount = 200,
                    }
                }
            };
        }


        [TestMethod()]
        public void BuildEmployeeDictionariesTest()
        {
            var dataManager = new DataManager
            {
                EmployeeHierarchy = GetTemplateStructure()
            };

            dataManager.BuildEmployeeDictionaries();

            Assert.IsNotNull(dataManager.Employees);
            Assert.IsTrue(dataManager.Employees.Count > 1);
            Assert.IsNotNull(dataManager.Employees[1]);
            Assert.IsNotNull(dataManager.Employees[2]);
            Assert.IsNotNull(dataManager.Employees[3]);
            Assert.IsNotNull(dataManager.Employees[4]);

            Assert.IsNull(dataManager.EmployeesBosses[1]);
            Assert.IsNotNull(dataManager.EmployeesBosses[2]);
            Assert.IsNotNull(dataManager.EmployeesBosses[3]);
            Assert.IsNotNull(dataManager.EmployeesBosses[4]);

            Assert.AreEqual(dataManager.EmployeesBosses[1], null);
            Assert.AreEqual(dataManager.EmployeesBosses[2], 1);
            Assert.AreEqual(dataManager.EmployeesBosses[3], 1);
            Assert.AreEqual(dataManager.EmployeesBosses[4], 3);
        }

        [TestMethod()]
        public void CalculateTransfersTest()
        {
            var dataManager = new DataManager
            {
                EmployeeHierarchy = GetTemplateStructure()
            };

            dataManager.BuildEmployeeDictionaries();
            dataManager.CalculateTransfers(GetTemplateTransfers());

            Assert.AreEqual(dataManager.Employees[1].MonetaryCommision, 300);
            Assert.AreEqual(dataManager.Employees[2].MonetaryCommision, 0);
            Assert.AreEqual(dataManager.Employees[3].MonetaryCommision, 150);
            Assert.AreEqual(dataManager.Employees[4].MonetaryCommision, 0);
        }

        [TestMethod()]
        public void GetAllSubordinatesWithoutTheirSubordinatesTest()
        {
            var dataManager = new DataManager
            {
                EmployeeHierarchy = GetTemplateStructure()
            };

            dataManager.BuildEmployeeDictionaries();

            Assert.AreEqual(dataManager.GetAllSubordinatesWithoutTheirSubordinates(1), 2);
            Assert.AreEqual(dataManager.GetAllSubordinatesWithoutTheirSubordinates(2), 0);
            Assert.AreEqual(dataManager.GetAllSubordinatesWithoutTheirSubordinates(3), 1);
            Assert.AreEqual(dataManager.GetAllSubordinatesWithoutTheirSubordinates(4), 0);
        }

        [TestMethod()]
        public void GetAllEmployeesSortedTest()
        {
            var dataManager = new DataManager
            {
                EmployeeHierarchy = GetTemplateStructure()
            };

            dataManager.BuildEmployeeDictionaries();
            var sorted = dataManager.GetAllEmployeesSorted();

            Assert.AreEqual(sorted[0].Id, 1);
            Assert.AreEqual(sorted[1].Id, 2);
            Assert.AreEqual(sorted[2].Id, 3);
            Assert.AreEqual(sorted[3].Id, 4);
        }
    }
}