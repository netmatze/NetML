using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace NetMLTests
{
    public class DataSetLoader
    {
        public List<Tuple<double[], double>> SelectAnimals()
        {
            var filename = @"\DataSets\AllAnimal.txt";
            List<Tuple<double[], double>> list = new List<Tuple<double[], double>>();
            var fullName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dataTable = Import(new FileInfo(fullName + filename), ',', Encoding.ASCII);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                double[] input = new double[16];
                double output = Convert.ToInt32(dataRow["type"].ToString());
                input[0] = Double.Parse(dataRow["hair"].ToString());
                input[1] = Double.Parse(dataRow["feathers"].ToString());
                input[2] = Double.Parse(dataRow["eggs"].ToString());
                input[3] = Double.Parse(dataRow["milk"].ToString());
                input[4] = Double.Parse(dataRow["airborne"].ToString());
                input[5] = Double.Parse(dataRow["aquatic"].ToString());
                input[6] = Double.Parse(dataRow["predator"].ToString());
                input[7] = Double.Parse(dataRow["toothed"].ToString());
                input[8] = Double.Parse(dataRow["backbone"].ToString());
                input[9] = Double.Parse(dataRow["breathes"].ToString());
                input[10] = Double.Parse(dataRow["venomous"].ToString());
                input[11] = Double.Parse(dataRow["fins"].ToString());
                input[12] = Double.Parse(dataRow["legs"].ToString());
                input[13] = Double.Parse(dataRow["tail"].ToString());
                input[14] = Double.Parse(dataRow["domestic"].ToString());
                input[15] = Double.Parse(dataRow["catsize"].ToString());
                list.Add(new Tuple<double[], double>(input, output));
            }
            return list;
        }

        public List<Tuple<double[], double>> SelectCrimes()
        {
            var filename = @"\DataSets\train.csv";
            List<Tuple<double[], double>> list = new List<Tuple<double[], double>>();
            values = new Dictionary<string, Dictionary<double, string>>();
            var fullName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dataTable = Import(new FileInfo(fullName + filename), ',', Encoding.ASCII);
            foreach (DataRow dataRow in dataTable.AsEnumerable().Take(100))
            {
                double[] input = new double[4];
                double output = ConvertDiskretValueToContinuationValue("Category", dataRow["Category"].ToString());
                if (!Categories.ContainsKey(output))
                {
                    Categories.Add(output, dataRow["Category"].ToString());
                }
                //input[0] = Double.Parse(dataRow["Dates"].ToString());                
                input[0] = ConvertDiskretValueToContinuationValue("Descript", dataRow["Descript"].ToString()); //Double.Parse(dataRow["Descript"].ToString());
                input[1] = ConvertDiskretValueToContinuationValue("DayOfWeek", dataRow["DayOfWeek"].ToString()); //Double.Parse(dataRow["DayOfWeek"].ToString());
                input[2] = ConvertDiskretValueToContinuationValue("PdDistrict", dataRow["PdDistrict"].ToString()); //Double.Parse(dataRow["PdDistrict"].ToString());
                input[3] = ConvertDiskretValueToContinuationValue("Resolution", dataRow["Resolution"].ToString()); //(dataRow["Resolution"].ToString());
                //input[5] = ConvertDiskretValueToContinuationValue("Address", dataRow["Address"].ToString()); //Double.Parse(dataRow["Address"].ToString());
                //input[6] = Double.Parse(dataRow["X"].ToString());
                //input[7] = Double.Parse(dataRow["Y"].ToString());                
                list.Add(new Tuple<double[], double>(input, output));
            }
            return list;
        }

        public List<Tuple<double[], double>> SelectCrimesTest()
        {
            var filename = @"\DataSets\test.csv";
            List<Tuple<double[], double>> list = new List<Tuple<double[], double>>();
            values = new Dictionary<string, Dictionary<double, string>>();
            var fullName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dataTable = Import(new FileInfo(fullName + filename), ',', Encoding.ASCII);
            foreach (DataRow dataRow in dataTable.AsEnumerable().Take(500))
            {
                double[] input = new double[2];
                double output = ConvertDiskretValueToContinuationValue("Id", dataRow["Id"].ToString());                
                //input[0] = Double.Parse(dataRow["Dates"].ToString());                
                //input[0] = ConvertDiskretValueToContinuationValue("Descript", dataRow["Descript"].ToString()); //Double.Parse(dataRow["Descript"].ToString());
                input[0] = ConvertDiskretValueToContinuationValue("DayOfWeek", dataRow["DayOfWeek"].ToString()); //Double.Parse(dataRow["DayOfWeek"].ToString());
                input[1] = ConvertDiskretValueToContinuationValue("PdDistrict", dataRow["PdDistrict"].ToString()); //Double.Parse(dataRow["PdDistrict"].ToString());
                //input[3] = ConvertDiskretValueToContinuationValue("Resolution", dataRow["Resolution"].ToString()); //(dataRow["Resolution"].ToString());
                //input[5] = ConvertDiskretValueToContinuationValue("Address", dataRow["Address"].ToString()); //Double.Parse(dataRow["Address"].ToString());
                //input[6] = Double.Parse(dataRow["X"].ToString());
                //input[7] = Double.Parse(dataRow["Y"].ToString());
                list.Add(new Tuple<double[], double>(input, output));
            }
            return list;

        }

        public List<Tuple<double[], double[]>> SelectNeuronalNetworkCrimes()
        {
            var filename = @"\DataSets\train.csv";
            List<Tuple<double[], double[]>> list = new List<Tuple<double[], double[]>>();
            values = new Dictionary<string, Dictionary<double, string>>();
            var fullName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dataTable = Import(new FileInfo(fullName + filename), ',', Encoding.ASCII);
            //var maxValue = Convert.ToInt32(dataTable.Compute("max(type)", string.Empty)) + 1;
            var maxValue = dataTable.AsEnumerable().Select(dr => dr["Category"]).Distinct().Count();
            foreach (DataRow dataRow in dataTable.AsEnumerable().Take(100))
            {
                double[] input = new double[2];
                double[] output = CalculateValue(Convert.ToInt32(ConvertDiskretValueToContinuationValue("Category", dataRow["Category"].ToString())), maxValue);
                input[0] = ConvertDiskretValueToContinuationValue("DayOfWeek", dataRow["DayOfWeek"].ToString()); //Double.Parse(dataRow["DayOfWeek"].ToString());
                input[1] = ConvertDiskretValueToContinuationValue("PdDistrict", dataRow["PdDistrict"].ToString()); //Double.Parse(dataRow["PdDistrict"].ToString());
                list.Add(new Tuple<double[], double[]>(input, output));
            }
            return list;
        }

        public Dictionary<double, string> Categories = new Dictionary<double, string>();

        private double[] CalculateValue(int value, int maxValue)
        {            
            double[] output = new double[maxValue];            
            for (int j = 0; j < 8; j++)
            {
                if (j == value - 1)
                    output[j] += 1;
            }
            return output;
        }

        public List<Tuple<double[], double[]>> SelectNeuronalNetworkAnimals()
        {
            var filename = @"\DataSets\AllAnimal.txt";
            List<Tuple<double[], double[]>> list = new List<Tuple<double[], double[]>>();
            var fullName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dataTable = Import(new FileInfo(fullName + filename), ',', Encoding.ASCII);
            var maxValue = Convert.ToInt32(dataTable.Compute("max(type)", string.Empty)) + 1;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                double[] input = new double[16];
                double[] output = CalculateValue(Convert.ToInt32(dataRow["type"]), maxValue);                
                input[0] = Double.Parse(dataRow["hair"].ToString());
                input[1] = Double.Parse(dataRow["feathers"].ToString());
                input[2] = Double.Parse(dataRow["eggs"].ToString());
                input[3] = Double.Parse(dataRow["milk"].ToString());
                input[4] = Double.Parse(dataRow["airborne"].ToString());
                input[5] = Double.Parse(dataRow["aquatic"].ToString());
                input[6] = Double.Parse(dataRow["predator"].ToString());
                input[7] = Double.Parse(dataRow["toothed"].ToString());
                input[8] = Double.Parse(dataRow["backbone"].ToString());
                input[9] = Double.Parse(dataRow["breathes"].ToString());
                input[10] = Double.Parse(dataRow["venomous"].ToString());
                input[11] = Double.Parse(dataRow["fins"].ToString());
                input[12] = Double.Parse(dataRow["legs"].ToString());
                input[13] = Double.Parse(dataRow["tail"].ToString());
                input[14] = Double.Parse(dataRow["domestic"].ToString());
                input[15] = Double.Parse(dataRow["catsize"].ToString());
                list.Add(new Tuple<double[], double[]>(
                    input, output));
            }
            return list;
        }

        public List<Tuple<double[], double>> SelectTrainingAnimals(double percent)
        {
            var animals = SelectAnimals();
            var resultAnimas = CalculateEntries(percent, animals);
            return resultAnimas;
        }

        public List<Tuple<double[], double>> SelectSelectingAnimals(double percent)
        {
            var animals = SelectAnimals();
            animals.Reverse();
            var resultAnimas = CalculateEntries(percent, animals);
            return resultAnimas;
        }

        public Tuple<List<Tuple<double[], double>>, List<Tuple<double[], double>>>
            CalculatePercent(double percent, List<Tuple<double[], double>> data)
        {
            var length = data.Count();
            var calcValue = length / 100.0 * percent;
            var maxValue = Math.Round(calcValue);
            var counter = 0;
            var trainingsData = new List<Tuple<double[], double>>();
            var testData = new List<Tuple<double[], double>>();
            foreach (var item in data)
            {
                if (counter > maxValue)
                    testData.Add(item);
                else
                    trainingsData.Add(item);
                counter++;
            }
            return new Tuple<List<Tuple<double[],double>>,List<Tuple<double[],double>>>(trainingsData, testData);
        }

        private List<Tuple<double[], double>> CalculateEntries(double percent, List<Tuple<double[], double>> animals)
        {
            var length = animals.Count();
            var calcValue = length / 100.0 * percent;
            var maxValue = Math.Round(calcValue);
            var counter = 0;
            var resultAnimas = new List<Tuple<double[], double>>();
            foreach (var item in animals)
            {
                resultAnimas.Add(item);
                counter++;
                if (counter > maxValue)
                    break;
            }
            return resultAnimas;
        }

        private List<Tuple<double[], double[]>> CalculateEntries(double percent, List<Tuple<double[], double[]>> animals)
        {
            var length = animals.Count();
            var calcValue = length / 100.0 * percent;
            var maxValue = Math.Round(calcValue);
            var counter = 0;
            var resultAnimas = new List<Tuple<double[], double[]>>();
            foreach (var item in animals)
            {
                resultAnimas.Add(item);
                counter++;
                if (counter > maxValue)
                    break;
            }
            return resultAnimas;
        }

        public List<Tuple<double[], double>> SelectTennis()
        {
            var filename = @"\DataSets\Tennis.txt";
            values = new Dictionary<string, Dictionary<double, string>>();
            List<Tuple<double[], double>> list = new List<Tuple<double[], double>>();
            var fullName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dataTable = Import(new FileInfo(fullName + filename), ';', Encoding.ASCII);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                double[] input = new double[3];
                double output = Convert.ToDouble(Boolean.Parse(dataRow["Play"].ToString()));
                input[0] = ConvertDiskretValueToContinuationValue("Outlook", dataRow["Outlook"].ToString());
                input[1] = ConvertDiskretValueToContinuationValue("Temperature", dataRow["Temperature"].ToString());
                input[2] = ConvertDiskretValueToContinuationValue("Windy", dataRow["Windy"].ToString());                
                list.Add(new Tuple<double[], double>(input, output));
            }
            return list;
        }

        public List<Tuple<double[], double>> SelectSalery()
        {
            var filename = @"\DataSets\salery.txt";
            values = new Dictionary<string, Dictionary<double, string>>();
            List<Tuple<double[], double>> list = new List<Tuple<double[], double>>();
            var fullName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dataTable = Import(new FileInfo(fullName + filename), ',', Encoding.ASCII);
            var dataView = dataTable.Select("salary <> 'NA'");
            foreach (DataRow dataRow in dataView)
            {
                //age,workclass,fnlwgt,education,education.num,marital.status,occupation,
                //relationship,race,sex,capital.gain,capital.loss,hours.per.week,native.country,salary
                double[] input = new double[14];
                double output = ConvertDiskretValueToContinuationValue("salary", dataRow["salary"].ToString());
                input[0] = Double.Parse(dataRow["age"].ToString());
                input[1] = ConvertDiskretValueToContinuationValue("workclass", dataRow["workclass"].ToString());
                input[2] = Double.Parse(dataRow["fnlwgt"].ToString());
                input[3] = ConvertDiskretValueToContinuationValue("education", dataRow["education"].ToString());
                input[4] = Double.Parse(dataRow["education.num"].ToString());
                input[5] = ConvertDiskretValueToContinuationValue("marital.status", dataRow["marital.status"].ToString());
                input[6] = ConvertDiskretValueToContinuationValue("occupation", dataRow["occupation"].ToString());
                input[7] = ConvertDiskretValueToContinuationValue("relationship", dataRow["relationship"].ToString());
                input[8] = ConvertDiskretValueToContinuationValue("race", dataRow["race"].ToString());
                input[9] = ConvertDiskretValueToContinuationValue("sex", dataRow["sex"].ToString());
                input[10] = Double.Parse(dataRow["capital.gain"].ToString());
                input[11] = Double.Parse(dataRow["capital.loss"].ToString());
                input[12] = Double.Parse(dataRow["hours.per.week"].ToString());
                input[13] = ConvertDiskretValueToContinuationValue("native.country", dataRow["native.country"].ToString());
                list.Add(new Tuple<double[], double>(input, output));
            }
            return list;
        }

        public DataTable SelectAllSalery()
        {
            var filename = @"\DataSets\salery.txt";
            values = new Dictionary<string, Dictionary<double, string>>();
            List<Tuple<double[], double>> list = new List<Tuple<double[], double>>();
            var fullName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dataTable = Import(new FileInfo(fullName + filename), ',', Encoding.ASCII);
            return dataTable;
        }

        public List<Tuple<double[], double>> SelectNotClassifiedSalery()
        {
            var filename = @"\DataSets\salery.txt";
            values = new Dictionary<string, Dictionary<double, string>>();
            List<Tuple<double[], double>> list = new List<Tuple<double[], double>>();
            var fullName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dataTable = Import(new FileInfo(fullName + filename), ',', Encoding.ASCII);
            var dataView = dataTable.Select("salary = 'NA'");
            foreach (DataRow dataRow in dataView)
            {
                //age,workclass,fnlwgt,education,education.num,marital.status,occupation,
                //relationship,race,sex,capital.gain,capital.loss,hours.per.week,native.country,salary
                double[] input = new double[14];
                double output = ConvertDiskretValueToContinuationValue("salary", dataRow["salary"].ToString());
                input[0] = Double.Parse(dataRow["age"].ToString());
                input[1] = ConvertDiskretValueToContinuationValue("workclass", dataRow["workclass"].ToString());
                input[2] = Double.Parse(dataRow["fnlwgt"].ToString());
                input[3] = ConvertDiskretValueToContinuationValue("education", dataRow["education"].ToString());
                input[4] = Double.Parse(dataRow["education.num"].ToString());
                input[5] = ConvertDiskretValueToContinuationValue("marital.status", dataRow["marital.status"].ToString());
                input[6] = ConvertDiskretValueToContinuationValue("occupation", dataRow["occupation"].ToString());
                input[7] = ConvertDiskretValueToContinuationValue("relationship", dataRow["relationship"].ToString());
                input[8] = ConvertDiskretValueToContinuationValue("race", dataRow["race"].ToString());
                input[9] = ConvertDiskretValueToContinuationValue("sex", dataRow["sex"].ToString());
                input[10] = Double.Parse(dataRow["capital.gain"].ToString());
                input[11] = Double.Parse(dataRow["capital.loss"].ToString());
                input[12] = Double.Parse(dataRow["hours.per.week"].ToString());
                input[13] = ConvertDiskretValueToContinuationValue("native.country", dataRow["native.country"].ToString());
                list.Add(new Tuple<double[], double>(input, output));
            }
            return list;
        }
        public List<Tuple<double[], double>> SelectComputerVendors()
        {
            var filename = @"\DataSets\ComputerVendors.txt";
            values = new Dictionary<string, Dictionary<double, string>>();
            List<Tuple<double[], double>> list = new List<Tuple<double[], double>>();
            var fullName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dataTable = Import(new FileInfo(fullName + filename), ',', Encoding.ASCII);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                double[] input = new double[9];
                double output = ConvertDiskretValueToContinuationValue("VendorName", dataRow["VendorName"].ToString());
                input[0] = ConvertDiskretValueToContinuationValue("ModelName", dataRow["ModelName"].ToString());
                input[1] = Double.Parse(dataRow["MYCT"].ToString());
                input[2] = Double.Parse(dataRow["MMIN"].ToString());
                input[3] = Double.Parse(dataRow["MMAX"].ToString());
                input[4] = Double.Parse(dataRow["CACH"].ToString());
                input[5] = Double.Parse(dataRow["CHMIN"].ToString());
                input[6] = Double.Parse(dataRow["CHMAX"].ToString());
                input[7] = Double.Parse(dataRow["PRP"].ToString());
                input[8] = Double.Parse(dataRow["ERP"].ToString());
                list.Add(new Tuple<double[], double>(input, output));
            }
            return list;
        }

        public List<Tuple<double[], double>> SelectTrainingComputerVendors(double percent)
        {
            var computerVendors = SelectComputerVendors();
            return CalculateEntries(percent, computerVendors);
        }

        public List<Tuple<double[], double>> SelectSelectingComputerVendors(double percent)
        {
            var computerVendors = SelectComputerVendors();
            computerVendors.Reverse();
            return CalculateEntries(percent, computerVendors);
        }

        public List<Tuple<double[], double>> SelectMushroom()
        {
            var filename = @"\DataSets\Mushroom.txt";
            values = new Dictionary<string, Dictionary<double, string>>();
            List<Tuple<double[], double>> list = new List<Tuple<double[], double>>();
            var fullName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dataTable = Import(new FileInfo(fullName + filename), ',', Encoding.ASCII);
            foreach (DataRow dataRow in dataTable.Rows)
            {                
                double[] input = new double[21];
                double output = ConvertDiskretValueToContinuationValue("classes", dataRow["classes"].ToString());
                input[0] = ConvertDiskretValueToContinuationValue("cap-surface", dataRow["cap-surface"].ToString());
                input[1] = ConvertDiskretValueToContinuationValue("cap-color", dataRow["cap-color"].ToString());
                input[2] = ConvertDiskretValueToContinuationValue("bruises", dataRow["bruises"].ToString());
                input[3] = ConvertDiskretValueToContinuationValue("odor", dataRow["odor"].ToString());
                input[4] = ConvertDiskretValueToContinuationValue("gill-attachment", dataRow["gill-attachment"].ToString());
                input[5] = ConvertDiskretValueToContinuationValue("gill-spacing", dataRow["gill-spacing"].ToString());
                input[6] = ConvertDiskretValueToContinuationValue("gill-size", dataRow["gill-size"].ToString());
                input[7] = ConvertDiskretValueToContinuationValue("gill-color", dataRow["gill-color"].ToString());
                input[8] = ConvertDiskretValueToContinuationValue("stalk-shape", dataRow["stalk-shape"].ToString());
                input[9] = ConvertDiskretValueToContinuationValue("stalk-root", dataRow["stalk-root"].ToString());
                input[10] = ConvertDiskretValueToContinuationValue("stalk-surface-above-ring", dataRow["stalk-surface-above-ring"].ToString());
                input[11] = ConvertDiskretValueToContinuationValue("stalk-surface-below-ring", dataRow["stalk-surface-below-ring"].ToString());
                input[12] = ConvertDiskretValueToContinuationValue("stalk-color-above-ring", dataRow["stalk-color-above-ring"].ToString());
                input[13] = ConvertDiskretValueToContinuationValue("stalk-color-below-ring", dataRow["stalk-color-below-ring"].ToString());
                input[14] = ConvertDiskretValueToContinuationValue("veil-type", dataRow["veil-type"].ToString());
                input[15] = ConvertDiskretValueToContinuationValue("veil-color", dataRow["veil-color"].ToString());
                input[16] = ConvertDiskretValueToContinuationValue("ring-number", dataRow["ring-number"].ToString());
                input[17] = ConvertDiskretValueToContinuationValue("ring-type", dataRow["ring-type"].ToString());
                input[18] = ConvertDiskretValueToContinuationValue("spore-print-color", dataRow["spore-print-color"].ToString());
                input[19] = ConvertDiskretValueToContinuationValue("population", dataRow["population"].ToString());
                input[20] = ConvertDiskretValueToContinuationValue("habitat", dataRow["habitat"].ToString());                
                list.Add(new Tuple<double[], double>(input, output));
            }
            return list;
        }

        public List<Tuple<double[], double>> SelectCreditData()
        {
            var filename = @"\DataSets\CreditData.txt";
            values = new Dictionary<string, Dictionary<double, string>>();
            List<Tuple<double[], double>> list = new List<Tuple<double[], double>>();
            var fullName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dataTable = Import(new FileInfo(fullName + filename), ' ', Encoding.ASCII);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                double[] input = new double[6];
                var outputValue = ConvertDiskretValueToContinuationValue("CreditRating", dataRow["CreditRating"].ToString());
                double output = outputValue;
                //StatusOfExistingCheckingAccount DurationInMonth CreditHistory Purpose CreditAmount SavingsAccount PresentEmploymentSince 
                //InstallmentRateInPercentageOfDisposableIncome PersonalStatusAndSex OtherDebtors PresentResidenceSince Property AgeInYears 
                //OtherInstallmentPlans Housing NumberOfExistingCreditsAtThisBank Job NumberOfPeopleBeingLiableToProvideMaintenanceFor Telephone ForeignWorker 
                input[0] = ConvertDiskretValueToContinuationValue("StatusOfExistingCheckingAccount", dataRow["StatusOfExistingCheckingAccount"].ToString());
                //input[1] = Double.Parse(dataRow["DurationInMonth"].ToString());
                input[1] = ConvertDiskretValueToContinuationValue("CreditHistory", dataRow["CreditHistory"].ToString());
                input[2] = ConvertDiskretValueToContinuationValue("Purpose", dataRow["Purpose"].ToString());
                //input[4] = Double.Parse(dataRow["CreditAmount"].ToString());
                input[3] = ConvertDiskretValueToContinuationValue("SavingsAccount", dataRow["SavingsAccount"].ToString());
                input[4] = ConvertDiskretValueToContinuationValue("PresentEmploymentSince", dataRow["PresentEmploymentSince"].ToString());
                //input[7] = Double.Parse(dataRow["InstallmentRateInPercentageOfDisposableIncome"].ToString());
                input[5] = ConvertDiskretValueToContinuationValue("PersonalStatusAndSex", dataRow["PersonalStatusAndSex"].ToString());
                //input[6] = ConvertDiskretValueToContinuationValue("OtherDebtors", dataRow["OtherDebtors"].ToString());
                //input[10] = Double.Parse(dataRow["PresentResidenceSince"].ToString());
                //input[11] = ConvertDiskretValueToContinuationValue("Property", dataRow["Property"].ToString());
                //input[12] = Double.Parse(dataRow["AgeInYears"].ToString());
                //input[13] = ConvertDiskretValueToContinuationValue("OtherInstallmentPlans", dataRow["OtherInstallmentPlans"].ToString());
                //input[14] = ConvertDiskretValueToContinuationValue("Housing", dataRow["Housing"].ToString());
                //input[15] = Double.Parse(dataRow["NumberOfExistingCreditsAtThisBank"].ToString());
                //input[16] = ConvertDiskretValueToContinuationValue("Job", dataRow["Job"].ToString());
                //input[17] = Double.Parse(dataRow["NumberOfPeopleBeingLiableToProvideMaintenanceFor"].ToString());
                //input[18] = ConvertDiskretValueToContinuationValue("Telephone", dataRow["Telephone"].ToString());
                //input[19] = ConvertDiskretValueToContinuationValue("ForeignWorker", dataRow["ForeignWorker"].ToString());
                list.Add(new Tuple<double[], double>(input, output));
            }
            return list;
        }

        public List<Tuple<double[], double[]>> SelectNeuronalNetworksCreditData()
        {
            var filename = @"\DataSets\CreditData.txt";
            values = new Dictionary<string, Dictionary<double, string>>();
            List<Tuple<double[], double[]>> list = new List<Tuple<double[], double[]>>();
            var fullName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dataTable = Import(new FileInfo(fullName + filename), ' ', Encoding.ASCII);
            var maxValue = 0;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                int value = (int)ConvertDiskretValueToContinuationValue("CreditRating", dataRow["CreditRating"].ToString());
                if (value > maxValue)
                    maxValue = value;
            }
            maxValue++;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                double[] input = new double[6];
                int outputValue = (int)ConvertDiskretValueToContinuationValue("CreditRating", dataRow["CreditRating"].ToString());
                double[] output = CalculateValue(++outputValue, maxValue); 
                input[0] = ConvertDiskretValueToContinuationValue("StatusOfExistingCheckingAccount", dataRow["StatusOfExistingCheckingAccount"].ToString());
                //input[1] = Double.Parse(dataRow["DurationInMonth"].ToString());
                input[1] = ConvertDiskretValueToContinuationValue("CreditHistory", dataRow["CreditHistory"].ToString());
                input[2] = ConvertDiskretValueToContinuationValue("Purpose", dataRow["Purpose"].ToString());
                //input[4] = Double.Parse(dataRow["CreditAmount"].ToString());
                input[3] = ConvertDiskretValueToContinuationValue("SavingsAccount", dataRow["SavingsAccount"].ToString());
                input[4] = ConvertDiskretValueToContinuationValue("PresentEmploymentSince", dataRow["PresentEmploymentSince"].ToString());
                //input[7] = Double.Parse(dataRow["InstallmentRateInPercentageOfDisposableIncome"].ToString());
                input[5] = ConvertDiskretValueToContinuationValue("PersonalStatusAndSex", dataRow["PersonalStatusAndSex"].ToString());
                //input[9] = ConvertDiskretValueToContinuationValue("OtherDebtors", dataRow["OtherDebtors"].ToString());
                //input[10] = Double.Parse(dataRow["PresentResidenceSince"].ToString());
                //input[11] = ConvertDiskretValueToContinuationValue("Property", dataRow["Property"].ToString());
                //input[12] = Double.Parse(dataRow["AgeInYears"].ToString());
                //input[13] = ConvertDiskretValueToContinuationValue("OtherInstallmentPlans", dataRow["OtherInstallmentPlans"].ToString());
                //input[14] = ConvertDiskretValueToContinuationValue("Housing", dataRow["Housing"].ToString());
                //input[15] = Double.Parse(dataRow["NumberOfExistingCreditsAtThisBank"].ToString());
                //input[16] = ConvertDiskretValueToContinuationValue("Job", dataRow["Job"].ToString());
                //input[17] = Double.Parse(dataRow["NumberOfPeopleBeingLiableToProvideMaintenanceFor"].ToString());
                //input[18] = ConvertDiskretValueToContinuationValue("Telephone", dataRow["Telephone"].ToString());
                //input[19] = ConvertDiskretValueToContinuationValue("ForeignWorker", dataRow["ForeignWorker"].ToString());
                list.Add(new Tuple<double[], double[]>(input, output));
            }
            return list;
        }


        public List<Tuple<double[], double[]>> SelectNeuronalNetworksMushroom()
        {
            var filename = @"\DataSets\Mushroom.txt";
            values = new Dictionary<string, Dictionary<double, string>>();
            List<Tuple<double[], double[]>> list = new List<Tuple<double[], double[]>>();
            var fullName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dataTable = Import(new FileInfo(fullName + filename), ',', Encoding.ASCII);
            var maxValue = 0;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                int value = (int)ConvertDiskretValueToContinuationValue("classes", dataRow["classes"].ToString());
                if (value > maxValue)
                    maxValue = value;
            }
            maxValue++;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                double[] input = new double[21];
                int outputValue = (int)ConvertDiskretValueToContinuationValue("classes", dataRow["classes"].ToString());
                double[] output = CalculateValue(++outputValue, maxValue);              
                input[0] = ConvertDiskretValueToContinuationValue("cap-surface", dataRow["cap-surface"].ToString());
                input[1] = ConvertDiskretValueToContinuationValue("cap-color", dataRow["cap-color"].ToString());
                input[2] = ConvertDiskretValueToContinuationValue("bruises", dataRow["bruises"].ToString());
                input[3] = ConvertDiskretValueToContinuationValue("odor", dataRow["odor"].ToString());
                input[4] = ConvertDiskretValueToContinuationValue("gill-attachment", dataRow["gill-attachment"].ToString());
                input[5] = ConvertDiskretValueToContinuationValue("gill-spacing", dataRow["gill-spacing"].ToString());
                input[6] = ConvertDiskretValueToContinuationValue("gill-size", dataRow["gill-size"].ToString());
                input[7] = ConvertDiskretValueToContinuationValue("gill-color", dataRow["gill-color"].ToString());
                input[8] = ConvertDiskretValueToContinuationValue("stalk-shape", dataRow["stalk-shape"].ToString());
                input[9] = ConvertDiskretValueToContinuationValue("stalk-root", dataRow["stalk-root"].ToString());
                input[10] = ConvertDiskretValueToContinuationValue("stalk-surface-above-ring", dataRow["stalk-surface-above-ring"].ToString());
                input[11] = ConvertDiskretValueToContinuationValue("stalk-surface-below-ring", dataRow["stalk-surface-below-ring"].ToString());
                input[12] = ConvertDiskretValueToContinuationValue("stalk-color-above-ring", dataRow["stalk-color-above-ring"].ToString());
                input[13] = ConvertDiskretValueToContinuationValue("stalk-color-below-ring", dataRow["stalk-color-below-ring"].ToString());
                input[14] = ConvertDiskretValueToContinuationValue("veil-type", dataRow["veil-type"].ToString());
                input[15] = ConvertDiskretValueToContinuationValue("veil-color", dataRow["veil-color"].ToString());
                input[16] = ConvertDiskretValueToContinuationValue("ring-number", dataRow["ring-number"].ToString());
                input[17] = ConvertDiskretValueToContinuationValue("ring-type", dataRow["ring-type"].ToString());
                input[18] = ConvertDiskretValueToContinuationValue("spore-print-color", dataRow["spore-print-color"].ToString());
                input[19] = ConvertDiskretValueToContinuationValue("population", dataRow["population"].ToString());
                input[20] = ConvertDiskretValueToContinuationValue("habitat", dataRow["habitat"].ToString());
                list.Add(new Tuple<double[], double[]>(input, output));
            }
            return list;
        }

        public List<Tuple<double[], double[]>> SelectNeuronalNetworksTrainingMushroom(double percent)
        {
            var mushroom = SelectNeuronalNetworksMushroom();
            var resultMushroom = CalculateEntries(percent, mushroom);
            return resultMushroom;
        }

        public List<Tuple<double[], double>> SelectNeuronalNetworksSelectingMushroom(double percent)
        {
            var mushroom = SelectMushroom();
            mushroom.Reverse();
            var resultMushroom = CalculateEntries(percent, mushroom);
            return resultMushroom;
        }

        public List<Tuple<double[], double>> SelectTrainingMushroom(double percent)
        {
            var mushroom = SelectMushroom();
            var resultAnimas = CalculateEntries(percent, mushroom);
            return resultAnimas;
        }

        public List<Tuple<double[], double>> SelectSelectingMushroom(double percent)
        {
            var mushroom = SelectMushroom();
            mushroom.Reverse();
            var resultAnimas = CalculateEntries(percent, mushroom);
            return resultAnimas;
        }

        public Dictionary<string, Dictionary<double, string>> values;

        private double ConvertDiskretValueToContinuationValue(string dictionaryValue, string value)
        {            
            if(values.ContainsKey(dictionaryValue))
            {
                var innerDictionary = values[dictionaryValue];
                if(innerDictionary.ContainsValue(value))
                {
                    foreach(var item in innerDictionary)
                    {                        
                        if (item.Value == value)
                            return item.Key;
                    }
                }
                else
                {
                    var index = innerDictionary.Count;
                    innerDictionary.Add(index, value);
                    return index;
                }
            }
            else
            {
                var innerDictionary = new Dictionary<double, string>();
                innerDictionary.Add(0.0, value);
                values.Add(dictionaryValue, innerDictionary);
            }
            return 0.0;
        }

        public List<Tuple<double[], double>> SelectSounds(string filename)
        {
            List<Tuple<double[], double>> list = new List<Tuple<double[], double>>();
            var fullName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var fileInfo = new FileInfo(fullName + filename);
            if(fileInfo.Exists)
            {
                var audioFile = File.ReadAllBytes(fileInfo.FullName);                
            }
            return list;
        }        

        public List<Tuple<double[], double>> SelectIrises()
        {
            var filename = @"\DataSets\Iris.txt";
            List<Tuple<double[], double>> list = new List<Tuple<double[], double>>();
            var fullName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dataTable = Import(new FileInfo(fullName + filename), ' ', Encoding.ASCII);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                double[] input = new double[4];
                double output = 0.0;
                input[0] = Double.Parse(dataRow["Sepallength"].ToString()) / 10.0;
                input[1] = Double.Parse(dataRow["Sepalwidth"].ToString()) / 10.0;
                input[2] = Double.Parse(dataRow["Petallength"].ToString()) / 10.0;
                input[3] = Double.Parse(dataRow["Petalwidth"].ToString()) / 10.0;
                var dataRowValue = dataRow["Species"].ToString();
                if (dataRowValue == "setosa")
                {
                    output = 1;
                }
                else if (dataRowValue == "versicolor")
                {
                    output = 2;
                }
                else if (dataRowValue == "virginica")
                {
                    output = 3;
                }
                list.Add(new Tuple<double[], double>(input, output));
            }
            return list;
        }

        public List<double[]> SelectClusteringIrises()
        {
            var filename = @"\DataSets\Iris.txt";
            List<double[]> list = new List<double[]>();
            var fullName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dataTable = Import(new FileInfo(fullName + filename), ' ', Encoding.ASCII);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                double[] input = new double[4];                
                input[0] = Double.Parse(dataRow["Sepallength"].ToString()) / 10.0;
                input[1] = Double.Parse(dataRow["Sepalwidth"].ToString()) / 10.0;
                input[2] = Double.Parse(dataRow["Petallength"].ToString()) / 10.0;
                input[3] = Double.Parse(dataRow["Petalwidth"].ToString()) / 10.0;                
                list.Add(input);
            }
            return list;
        }

        public List<Tuple<double[], double[]>> SelectNeuronalNetworkIrises()
        {
            var filename = @"\DataSets\Iris.txt";
            List<Tuple<double[], double[]>> list = new List<Tuple<double[], double[]>>();
            var fullName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dataTable = Import(new FileInfo(fullName + filename), ' ', Encoding.ASCII);            
            foreach (DataRow dataRow in dataTable.Rows)
            {
                double[] input = new double[4];
                double[] output = new double[3];
                input[0] = Double.Parse(dataRow["Sepallength"].ToString()) / 10.0;
                input[1] = Double.Parse(dataRow["Sepalwidth"].ToString()) / 10.0;
                input[2] = Double.Parse(dataRow["Petallength"].ToString()) / 10.0;
                input[3] = Double.Parse(dataRow["Petalwidth"].ToString()) / 10.0;
                var dataRowValue = dataRow["Species"].ToString();
                if (dataRowValue == "setosa")
                {
                    output[0] = 1;
                }
                else if (dataRowValue == "versicolor")
                {
                    output[1] = 2;
                }
                else if (dataRowValue == "virginica")
                {
                    output[2] = 3;
                }
                list.Add(new Tuple<double[], double[]>(
                    input, output));
            }
            return list;
        }

        public List<Tuple<double[], double>> SelectTrainingIrises(double percent)
        {
            var irises = SelectIrises();
            var resultIrises = CalculateEntries(percent, irises);
            return resultIrises;
        }

        public List<Tuple<double[], double>> SelectSelectingIrises(double percent)
        {
            var irises = SelectIrises();
            irises.Reverse();
            var resultIrises = CalculateEntries(percent, irises);
            return resultIrises;
        }

        public DataTable Import(FileInfo fileInfo, char splitChar, Encoding encoding)
        {
            var plainfileName = Path.GetFileName(fileInfo.FullName);
            DataTable dataTable = new DataTable(plainfileName);
            Func<string[]> allRowsFunc = () => File.ReadAllLines(fileInfo.FullName, encoding); // Encoding.Default
            var allRows = allRowsFunc();
            var columnHeaders = allRows.
                Select(str => str.TrimEnd().Split(splitChar)).
                First().
                Select(columnHeader => { return new DataColumn(columnHeader.Trim()); }).
                Select(column =>
                {
                    dataTable.Columns.Add(column);
                    return column;
                }).
                ToList();
            var allDataRows = allRows.
                Select(str => str.Split(splitChar)).
                Skip(1).
                Select(strArray =>
                {
                    List<string> strings = new List<string>();
                    foreach (var s in strArray)
                        strings.Add(s.Trim());
                    return strings.ToArray();
                }).
                Where(st => st.Length <= dataTable.Columns.Count).
                Select(st =>
                {
                    if (st.Length <= dataTable.Columns.Count)
                        return dataTable.Rows.Add(st);
                    else
                    {
                        var list = st.ToList();
                        list.RemoveAt(st.Length - 1);
                        st = list.ToArray();
                        return dataTable.Rows.Add(st);
                    }
                }
                ).
                ToList();
            return dataTable;
        }

        public string[] ToCSV(DataTable table)
        {
            List<string> resultList = new List<string>();
            var result = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                result.Append(table.Columns[i].ColumnName);
                result.Append(i == table.Columns.Count - 1 ? "\n" : ",");
            }
            resultList.Add(result.ToString());

            foreach (DataRow row in table.Rows)
            {
                result = new StringBuilder();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    result.Append(row[i].ToString());
                    result.Append(i == table.Columns.Count - 1 ? "\n" : ",");
                }
                resultList.Add(result.ToString());
            }
            return resultList.ToArray();
        }

        public void Export(DataTable dataTable, FileInfo fileInfo)
        {
            var plainfileName = Path.GetFileName(fileInfo.FullName);
            var lines = ToCSV(dataTable);
            File.WriteAllLines(plainfileName, lines);
        }
    }
}
