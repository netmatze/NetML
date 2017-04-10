using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netMLLanguage.Parser
{
    public class NetMLParser
    {
        public NetMLObject Parse(string netMLString)
        {
            var netMLObjectBuilder = new NetMLObjectBuilder();
            var netMLObject = ParserObject(netMLObjectBuilder);
            var result = netMLObject(netMLString);
            return netMLObjectBuilder.MainNetMLObject;
        }

        private Parse<string> ParserObject(NetMLObjectBuilder netMLObjectBuilder)
        {
            var createKNN = CreateKNN(netMLObjectBuilder);

            var createLogisticregression = CreateLogisticregression(netMLObjectBuilder);

            var createdecisiontree = CreateDecisiontree(netMLObjectBuilder);

            var createrandomforest = CreateRandomforest(netMLObjectBuilder);

            var createnaivebayers = CreateNaivebayers(netMLObjectBuilder);

            Parse<string> backpropagation;
            Parse<string> radialbasisfunction;
            Parse<string> selforganisingmap;
            NeuronalNetworks(netMLObjectBuilder, out backpropagation, out radialbasisfunction, out selforganisingmap);

            Parse<string> supportvectormachine;
            Parse<string> dualperceptron;
            CreateSupportVectorMachine(netMLObjectBuilder, out supportvectormachine, out dualperceptron);

            var KMeans = CreateKMeans(netMLObjectBuilder);

            var linearregression = CreateLinearregression(netMLObjectBuilder);

            var apriori = CreateApriori(netMLObjectBuilder);

            var createClassification = Parser.Literal("classification").
                CreateClassificationAlgorithmusObject(netMLObjectBuilder).
                Then(_ => createKNN.
                    Or(createLogisticregression).
                    Or(createdecisiontree).
                    Or(createrandomforest).
                    Or(createnaivebayers).
                    Or(supportvectormachine).
                    Or(backpropagation).
                    Or(radialbasisfunction).
                    Or(selforganisingmap));

            //var createClustering = Parser.Literal("clustering").CreateClassificationAlgorithmusObject(netMLObjectBuilder)
            //    .Then(_ => KMeans);

            var createClustering = CreateClustering(netMLObjectBuilder);

            var createRegression = Parser.Literal("regression").CreateClassificationAlgorithmusObject(netMLObjectBuilder)
               .Then(_ => linearregression);

            var createAssociation = Parser.Literal("association").CreateClassificationAlgorithmusObject(netMLObjectBuilder)
               .Then(_ => apriori);

            var createLiteral = Parser.StringLiteral("create").CreateNetMLObject(netMLObjectBuilder).
                Then(_ => createClassification.Or(createClustering).Or(createRegression).Or(createAssociation));

            var doubleParserValue = Parser.DecimalString().CreateValue(netMLObjectBuilder);

            var integerParserValue = Parser.Integer().CreateValue(netMLObjectBuilder);

            var variable = Parser.StringValue().CreateVariable(netMLObjectBuilder);

            return createLiteral;
        }

        private Parse<string> CreateClustering(NetMLObjectBuilder netMLObjectBuilder)
        {
            var knneuclidmetric = Parser.Literal("euclidmetric").CreateOption(netMLObjectBuilder);

            var knnmanhattanmetric = Parser.Literal("manhattanmetric").CreateOption(netMLObjectBuilder);

            var knnmaximummetric = Parser.Literal("maximummetric").CreateOption(netMLObjectBuilder);

            var knnsquardeuclidmetric = Parser.Literal("squaredeuclidmetric").CreateOption(netMLObjectBuilder);

            var KMeans = CreateKMeans(netMLObjectBuilder);

            var KMetroids = CreateKmetroids(netMLObjectBuilder);

            var createClustering = Parser.Literal("clustering").CreateClassificationAlgorithmusObject(netMLObjectBuilder)
                .Then(_ => KMeans.Or(KMetroids).Then(__ => knneuclidmetric.Or(knnmanhattanmetric).Or(knnmaximummetric).Or(knnsquardeuclidmetric)));
            return createClustering;
        }

        private Parse<string> CreateKMeans(NetMLObjectBuilder netMLObjectBuilder)
        {
            var createKMeans = Parser.Literal("kmeans").CreateAlgorithmus(netMLObjectBuilder);
            return createKMeans;
        }

        private Parse<string> CreateKmetroids(NetMLObjectBuilder netMLObjectBuilder)
        {
            var createKmetroids = Parser.Literal("kmetroids").CreateAlgorithmus(netMLObjectBuilder);
            return createKmetroids;
        }

        private Parse<string> CreateLinearregression(NetMLObjectBuilder netMLObjectBuilder)
        {
            var linearregression = Parser.Literal("linearregression").CreateAlgorithmus(netMLObjectBuilder);
            return linearregression;
        }

        private Parse<string> CreateApriori(NetMLObjectBuilder netMLObjectBuilder)
        {
            var apriori = Parser.Literal("apriori").CreateAlgorithmus(netMLObjectBuilder);
            return apriori;
        }

        private void CreateSupportVectorMachine(NetMLObjectBuilder netMLObjectBuilder,
            out Parse<string> supportvectormachine, out Parse<string> dualperceptron)
        {
            var linearkernel = Parser.Literal("linearkernel").CreateOption(netMLObjectBuilder);
            var gaussiankernel = Parser.Literal("gaussiankernel").CreateOption(netMLObjectBuilder);
            var polynomialkernel = Parser.Literal("polynomialkernel").CreateOption(netMLObjectBuilder);
            var logitkernel = Parser.Literal("logitkernel").CreateOption(netMLObjectBuilder);
            var tanhkernel = Parser.Literal("tanhkernel").CreateOption(netMLObjectBuilder);
            var n = Parser.Literal("n").CreateVariable(netMLObjectBuilder).
                Then(_ => Parser.Literal("=").And(Parser.DecimalString().CreateValue(netMLObjectBuilder)));
            var c = Parser.Literal("c").CreateVariable(netMLObjectBuilder).
                Then(_ => Parser.Literal("=").And(Parser.DecimalString().CreateValue(netMLObjectBuilder)));

            var kernel = linearkernel.Or(gaussiankernel).Or(polynomialkernel).Or(logitkernel).Or(tanhkernel);

            dualperceptron = Parser.Literal("dualperceptron").CreateAlgorithmus(netMLObjectBuilder).Then(_ =>
                kernel);

            supportvectormachine = Parser.Literal("supportvectormachine").CreateAlgorithmus(netMLObjectBuilder).Then(_ =>
                kernel).Then(_x => n).Then(_xx => c);            
        }

        private void NeuronalNetworks(NetMLObjectBuilder netMLObjectBuilder,
            out Parse<string> backpropagation, out Parse<string> radialbasisfunction, out Parse<string> selforganisingmap)
        {
            var inputneurons = Parser.Literal("inputneurons").CreateVariable(netMLObjectBuilder).
                Then(_ => Parser.Literal("=").And(Parser.IntegerString().CreateValue(netMLObjectBuilder)));

            var outputneurons = Parser.Literal("outputneurons").CreateVariable(netMLObjectBuilder).
                Then(_ => Parser.Literal("=").And(Parser.IntegerString().CreateValue(netMLObjectBuilder)));

            var firsthiddenlayerneurons = Parser.Literal("firsthiddenlayerneurons").CreateVariable(netMLObjectBuilder).
                Then(_ => Parser.Literal("=").And(Parser.IntegerString().CreateValue(netMLObjectBuilder)));

            var secondhiddenlayerneurons = Parser.Literal("secondhiddenlayerneurons").CreateVariable(netMLObjectBuilder).
                Then(_ => Parser.Literal("=").And(Parser.IntegerString().CreateValue(netMLObjectBuilder)));

            var evolutions = Parser.Literal("evolutions").CreateVariable(netMLObjectBuilder).
                Then(_ => Parser.Literal("=").And(Parser.IntegerString().CreateValue(netMLObjectBuilder)));

            var learningrate = Parser.Literal("learningrate").CreateVariable(netMLObjectBuilder).
                Then(_ => Parser.Literal("=").And(Parser.DecimalString().CreateValue(netMLObjectBuilder)));

            backpropagation = Parser.Literal("backpropagation").CreateAlgorithmus(netMLObjectBuilder).
                Then(_ => inputneurons.And(outputneurons).
                  Then(_x => firsthiddenlayerneurons).
                  Then(_xx => secondhiddenlayerneurons).
                  Then(_xxx => evolutions).
                  Then(_xxxx => learningrate));

            radialbasisfunction = Parser.Literal("radialbasisfunction").CreateAlgorithmus(netMLObjectBuilder).
                Then(_ => inputneurons.And(outputneurons).
                    Then(_x => firsthiddenlayerneurons).
                    Then(_xxx => evolutions).
                    Then(_xxxx => learningrate));

            selforganisingmap = Parser.Literal("selforganisingmap").CreateAlgorithmus(netMLObjectBuilder).
                Then(_ => inputneurons.And(outputneurons));
        }

        private Parse<string> CreateNaivebayers(NetMLObjectBuilder netMLObjectBuilder)
        {
            var linearbayeskernel = Parser.Literal("linearbayeskernel").CreateOption(netMLObjectBuilder);

            var gaussianbayeskernel = Parser.Literal("gaussianbayeskernel").CreateOption(netMLObjectBuilder);

            var createnaivebayers = Parser.Literal("naivebayers").CreateAlgorithmus(netMLObjectBuilder).
                Then(_ => linearbayeskernel.Or(gaussianbayeskernel));
            return createnaivebayers;
        }

        private Parse<string> CreateLogisticregression(NetMLObjectBuilder netMLObjectBuilder)
        {
            var logitcostfunction = Parser.Literal("logitcostfunction").CreateOption(netMLObjectBuilder);

            var tanhcostfunction = Parser.Literal("tanhcostfunction").CreateOption(netMLObjectBuilder);

            var logisticregression = Parser.Literal("logisticregression").CreateAlgorithmus(netMLObjectBuilder).
                Then(_ => logitcostfunction.Or(tanhcostfunction));
            return logisticregression;
        }

        private Parse<string> CreateDecisiontree(NetMLObjectBuilder netMLObjectBuilder)
        {
            var decisiontreeshannonentropysplitter = Parser.Literal("shannonentropysplitter").CreateOption(netMLObjectBuilder);

            var decisiontreeginiindexsplitter = Parser.Literal("giniindexsplitter").CreateOption(netMLObjectBuilder);

            var createdecisiontree = Parser.Literal("decisiontree").CreateAlgorithmus(netMLObjectBuilder).
               Then(_ => decisiontreeshannonentropysplitter.Or(decisiontreeginiindexsplitter)); //.Or(Parser.Literal("")
            return createdecisiontree;
        }

        private Parse<string> CreateRandomforest(NetMLObjectBuilder netMLObjectBuilder)
        {
            var decisiontreeshannonentropysplitter = Parser.Literal("shannonentropysplitter").CreateOption(netMLObjectBuilder);

            var decisiontreeginiindexsplitter = Parser.Literal("giniindexsplitter").CreateOption(netMLObjectBuilder);

            var bagging = Parser.Literal("bagging").CreateOption(netMLObjectBuilder);

            var boosting = Parser.Literal("boosting").CreateOption(netMLObjectBuilder);

            var randomforestOption = bagging.Or(boosting);

            var randomforest = Parser.Literal("Randomforest").CreateAlgorithmus(netMLObjectBuilder).
               Then(_ => decisiontreeshannonentropysplitter.Or(decisiontreeginiindexsplitter)).Then(_x => randomforestOption);
            return randomforest;
        }

        private Parse<string> CreateKNN(NetMLObjectBuilder netMLObjectBuilder)
        {
            var knneuclidmetric = Parser.Literal("euclidmetric").CreateOption(netMLObjectBuilder);

            var knnmanhattanmetric = Parser.Literal("manhattanmetric").CreateOption(netMLObjectBuilder);

            var knnmaximummetric = Parser.Literal("maximummetric").CreateOption(netMLObjectBuilder);

            var knnsquardeuclidmetric = Parser.Literal("squaredeuclidmetric").CreateOption(netMLObjectBuilder);

            var createKNN = Parser.Literal("knn").CreateAlgorithmus(netMLObjectBuilder).
                Then(_ => knneuclidmetric.Or(knnmanhattanmetric).Or(knnmaximummetric).Or(knnsquardeuclidmetric));
            return createKNN;
        }
    }
}


//var end = Parser.Literal("}").EndJsonObject(netMLObjectBuilder);

//var stringParseName = Parser.Literal("'").
//    And_(_ => Parser.StringValue().CreateName(netMLObjectBuilder).
//    And_(__ => Parser.Literal("'")));

//var parseName = Parser.StringValue().CreateName(netMLObjectBuilder);

//var stringParseValueOrParseName = stringParseName.Or(parseName);

//var doubleParserValue = Parser.DecimalString().CreateValue(netMLObjectBuilder);

//var boolParserValue = Parser.BoolString().CreateValue(netMLObjectBuilder);

//var stringParseValue = Parser.Literal("'").
//   And_(_ => Parser.StringValue().CreateValue(netMLObjectBuilder).
//   And_(__ => Parser.Literal("'")));

//var parseValue = Parser.StringValue().CreateValue(netMLObjectBuilder);

//var stringParseValueOrParseValue = stringParseValue.Or(parseValue).Or(doubleParserValue);

//var stringArrayParseValue = Parser.Literal("'").
//   And_(_ => Parser.StringValue().CreateArrayValue(netMLObjectBuilder).
//   And_(__ => Parser.Literal("'")));

//var parseArrayValue = Parser.StringValue().CreateArrayValue(netMLObjectBuilder);

//var doubleArrayParserValue = Parser.DecimalString().CreateArrayValue(netMLObjectBuilder);

//var stringArrayParseValueOrParseValue = stringArrayParseValue.Or(parseArrayValue).Or(doubleArrayParserValue);

//var keyStringValuePairObject =
//    stringParseValueOrParseName.Then_(_ => Parser.Literal(":").Then_(__ => stringParseValueOrParseValue));

//var innerValuePropertyObject = stringParseValueOrParseValue.
//    Then_(_ => Parser.Literal(","));

//var innerArrayValuePropertyObject = stringArrayParseValueOrParseValue.
//   Then_(_ => Parser.Literal(","));

//var keyStringArrayValueObject = stringParseValueOrParseName.Then_(_ => Parser.Literal(":")).
//    Then_(__ =>
//    Parser.Literal("[").CreateArray(netMLObjectBuilder).
//    Repeat(innerArrayValuePropertyObject).Then_(_ => Parser.Literal("]")));

//var keyStringPropertyOrKeyStringArray = keyStringArrayValueObject.Or(keyStringValuePairObject);

//return keyStringPropertyOrKeyStringArray.
//    Then_(_ => Parser.Literal(":")).
//    Then_(_ => start).
//    Then_(__ => end.Or(keyStringValuePairObject).Or(keyStringArrayValueObject).Or(ParserObject(netMLObjectBuilder))).
//    Repeat(Parser.Literal(",").
//    And_(_ => end.Or(keyStringValuePairObject).Or(keyStringArrayValueObject).Or(ParserObject(netMLObjectBuilder)))).
//    Then_(___ => end);
