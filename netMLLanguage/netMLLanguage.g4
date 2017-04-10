grammar netMLLanguage;


/*
	Lexer Rules
*/

	MINUS : '-';   
    POINT : '.';
    IS : '=';
    TRUE : 'true';
    FALSE : 'false';
    NULL : 'null';
    DOUBLEQUOTE : '"';    
    CREATE : 'create';
    CLASSIFICATION : 'classification';
    KNN : 'knn';
    EUCLIDMETRIC : 'euclidmetric';
    MANHATTANMETRIC : 'manhattanmetric';
    DECISIONTREE : 'decisiontree';
    SHANNONENTROPYSPLITTER : 'shannonentropysplitter';
    GINIINDEXSPLITTER : 'giniindexsplitter';
    RANDOMFOREST : 'randomforest';
	BAGGING : 'bagging';
	BOSTING : 'boosting';
    BACKPROPAGATION : 'backpropagation';
	RADIALBASISFUNCTION : 'radialbasisfunction';
    SELFORGANISINGMAP : 'selforganisingmap';
    INPUTNEURONS : 'inputneurons';
    OUTPUTNEURONS : 'outputneurons';
    FIRSTHIDDENLAYERNEURONS : 'firsthiddenlayerneurons';
    SECONDHIDDENLAYERNEURONS : 'secondhiddenlayerneurons';
    EVOLUTIONS : 'evolutions';
    LEARNINGRATE : 'learningrate';
    LOGISTICREGRESSION : 'logisticregression';
    LOGITCOSTFUNCTION : 'logitcostfunction';
    TANHCOSTFUNCTION : 'tanhcostfunction';
    NAIVEBAYERS : 'naivebayers';
    LINEARBAYESKERNEL : 'linearbayeskernel';
    GAUSSIANBAYESKERNEL : 'gaussianbayeskernel';
    DUALPERCEPTRON : 'dualperceptron';
    SUPPORTVECTORMACHINE : 'supportvectormachine';
    LINEARKERNEL : 'linearkernel';
    GAUSSIANKERNEL : 'gaussiankernel';
    POLYNOMIALKERNEL : 'polynomialkernel';
    LOGITKERNEL : 'logitkernel';
    TANHKERNEL : 'tanhkernel';
    CLUSTERING : 'clustering';
    KMEANS : 'kmeans';
    ASSOCIATION : 'association';
    APRIORI : 'apriori';
    REGRESSION : 'regression';
    LINEARREGRESSION : 'linearregression';   
    N : 'n';
    C : 'c';
    WS : (' ' | '\t' | '\n' | '\r' | '\f')+ ;  
    INTEGER : '0'..'9'+
      ;
    
/*
	Parser Rules
*/

	double :
        MINUS? INTEGER POINT INTEGER
        ;
    
    netmlexpression  :
        CREATE (classification | regression | clustering | association)
        ;  
       
    classification :
        CLASSIFICATION (knn | 
						decisiontree | 
						logisticregression | 
                        naivebayers | 
						randomforest | 
						dualperceptron | 
                        supportvectormachine | 
						backpropagation | 
						radialbasisfunction |
                        selforganisingmap)
        ;
    
    knn :
        KNN (EUCLIDMETRIC | MANHATTANMETRIC)?
        ;
    
    logisticregression :
        LOGISTICREGRESSION (LOGITCOSTFUNCTION | TANHCOSTFUNCTION)?
        ;
    
    decisiontree :
        DECISIONTREE (SHANNONENTROPYSPLITTER | GINIINDEXSPLITTER)?
        ;
    
    naivebayers :
        NAIVEBAYERS bayeskernel?            
        ;
    
    bayeskernel : 
        LINEARBAYESKERNEL | GAUSSIANBAYESKERNEL
        ;
    
    dualperceptron :
        DUALPERCEPTRON kernel?                
        ;
    
    supportvectormachine :
        SUPPORTVECTORMACHINE kernel? (N IS double)? (C IS double)?               
        ;
    
    kernel :
        LINEARKERNEL | 
		GAUSSIANKERNEL | 
		POLYNOMIALKERNEL | 
        LOGITKERNEL | 
		TANHKERNEL
        ;
    
    randomforest :
        RANDOMFOREST (SHANNONENTROPYSPLITTER | GINIINDEXSPLITTER)? randomforestOption?
        ;

	randomforestOption :
        BAGGING | 
		BOSTING		
        ;
    
    backpropagation : 
        BACKPROPAGATION 
        INPUTNEURONS IS INTEGER 
        OUTPUTNEURONS IS INTEGER 
        (FIRSTHIDDENLAYERNEURONS IS INTEGER)? 
        (SECONDHIDDENLAYERNEURONS IS INTEGER)? 
        (EVOLUTIONS IS INTEGER)? 
        (LEARNINGRATE IS double)?       
        ;

	radialbasisfunction :          
        INPUTNEURONS IS INTEGER 
        OUTPUTNEURONS IS INTEGER 
        FIRSTHIDDENLAYERNEURONS IS INTEGER
        (EVOLUTIONS IS INTEGER)? 
        (LEARNINGRATE IS double)?       
        ;
    
    selforganisingmap : 
        SELFORGANISINGMAP 
        INPUTNEURONS IS INTEGER 
        OUTPUTNEURONS IS INTEGER
        ;
    
    regression :
        REGRESSION LINEARREGRESSION?
        ;
         
    clustering :
        CLUSTERING KMEANS?
        ;
    
    association :
        ASSOCIATION APRIORI?
        ;
