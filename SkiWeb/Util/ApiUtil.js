//APICALLER
/*
    * All communication with API controllers goes through an APICaller tailormade for that one controller.
    * These are created by calling the makeAPICaller function.
    * All communication is sent using request and response models.
    * These models are defined by model schemas, implemented in the initModels function.
    * A model schema have to be implemented according to the following rules:
    * - All models have to implement the property [name], which value have to be identical to the model name
    * - The name of Models representing responses and the value of the property name 
    *   - have to be identical to their corresponding response class in the controller
    * If a request or response does not correspond with an existing model, an error is thrown
    * 
    * Communication with the controller is implemented through request functions.
    * Each function corresponds to 1 API method in the controller.
    * These functions should take the following input:
    * - model: a model object representing the request (properties should be the same as the method parameters of the corresponding controller method)
    * - onSuccess(model): what happens on a succesfull request
    * - onFail(error): what happens on a failed request
    * These functions can then use the predefined CRUD functions in makeApiCaller
    * to send requests:
    * - CREATE, READ, UPDATE, DESTROY
    * The functions are implemented in their corresponding init[ApiCallerName]Functions, which in turn is implemented inside the initAPICaller function
    *
    * Adding a new APICaller requires the following:
    * 1. Add the APICallers name as a property ([ApiCallerName]:[ApiCallerName]) inside APICallerNames, 
    *    - should be the same name as the corresponding controller    
    * 2. Add an init[ApiCallerName]Functions inside the initAPICaller function. This should add request functions to the APICaller
    * 3. Add a switchase on the APICaller name calling the init function inside of the initAPICaller function
*/
const APIUtil = {
    baseUrl: "https://localhost:44386",
    APICallerNames: {
        skiCalculator: 'skiCalculator',
    },
    makeAPICaller: function (apiCallerName) {
        try {
            var models;
            initModels();
            var apiCallerBaseUrl = new URL(APIUtil.baseUrl + '/api/' + apiCallerName);
            var apiCaller = {
                modelSchemas: {},
                addModelSchema: function (ms) {
                    if (!ms.name)
                        throw Util.makeError("model does not implement requered property name!")
                    if (this.modelSchemas[ms.name])
                        throw Util.makeError("model schema " + ms.name + " already exists!")
                    this.modelSchemas[ms.name] = ms;
                },
                makeModel: function (modelSchema) {
                    var model = {};
                    Object.assign(model, modelSchema);
                    return model;
                },
                isModel: function (model) {
                    try {
                        expectedParams = this.modelSchemas[model.name];
                        var expectedParams = Object.keys(expectedParams);
                        var actualParams = Object.keys(model);
                        if (actualParams.length != expectedParams.length)
                            return false;
                        return actualParams.filter(ap => !expectedParams.includes(ap)) == 0;
                    } catch (e) {
                        return false;
                    }
                }
            };
            initAPICaller(apiCaller, apiCallerName);
            return apiCaller;
        } catch (e) {
            throw Util.makeError(e)
        }

        //ignoreNameProp: Since all models have the property "name", it is possible to ignore it.
        //all property names are set to lower case, to fit the camelCase used in c# methods
        function makeUrlParamString(params, ignoreNameProp) {
            var paramsString = "?";
            Object.keys(params).forEach(key => {
                if (ignoreNameProp && key == "Name")
                    return;
                paramsString += '&' + key.toLowerCase() + '=' + params[key];
            });
            return paramsString;
        };
        function create(url, model, onSuccess, onFail) {
            callApi('POST', url, model, onSuccess, onFail);
        }
        function read(url, model, onSuccess, onFail) {
            callApi('GET', url, model, onSuccess, onFail);
        }
        function update(url, model, onSuccess, onFail) {
            callApi('PATCH', url, model, onSuccess, onFail);
        }
        function destroy(url, model, onSuccess, onFail) {
            callApi('DELETE', url, model, onSuccess, onFail);
        }
        function callApi(method, url, model, onSuccess, onFail) {
            if (!apiCaller.isModel(model))
                throw Util.makeError("requestModel does not match any requestModelSchema!")
            var config;
            if (method != 'GET') {
                config = {
                    method: method,
                    body: JSON.stringify(model)
                };
            }
            fetch(url, config)
                .then((response) => {
                    if (response.ok)
                        return response.json();
                    else
                        throw Util.makeError(response);
                })
                .then((model) => {
                    if (!apiCaller.isModel(model))
                        throw Util.makeError("responseModel does not match any requestModelSchema!")
                    onSuccess(model);
                })
                .catch((error) => {
                    onFail(error);
                });
        }
        function initAPICaller(apiCaller, apiCallerName) {
            switch (apiCallerName) {
                case APIUtil.APICallerNames.skiCalculator:
                    initSkiCalculatorFunctions(apiCaller);
                    break;
                default:
                    throw Util.makeError("Unknown APICaller name!")
            }
            Object.keys(models[apiCallerName]).forEach(key => {
                apiCaller.addModelSchema(models[apiCallerName][key]);
            });
            //Init APICaller Functions
            function initSkiCalculatorFunctions() {
                apiCaller['GETSkiRecommendation'] = function (model, onSuccess, onFail) {
                    var url = apiCallerBaseUrl + '/getskirecommendation';
                    url += makeUrlParamString(model, true)
                    read(url, model, onSuccess, onFail);
                };
                apiCaller['GETSkiStyles'] = function (model, onSuccess, onFail) {
                    var url = new URL(apiCallerBaseUrl + '/getskistyles');
                    read(url, model, onSuccess, onFail);
                };
            }
        }
        function initModels() {
            models = {
                skiCalculator: {
                    SkiRecommendation: {
                        name: "SkiRecommendation",
                        age: null,
                        height: null,
                        style: null
                    },
                    ExactLengthSkiRecommendationResponse: {
                        name: "ExactLengthSkiRecommendationResponse",
                        exactLength: null,
                        unit: null,
                    },
                    LengthSpanSkiRecommendationResponse: {
                        name: "LengthSpanSkiRecommendationResponse",
                        lengthFrom: null,
                        lengthTo: null,
                        unit: null,
                    },
                    NoLengthRecommendationResponse: {
                        name: "NoLengthRecommendationResponse",
                        noRecommendation:null,
                    },
                    SkiStyles: {
                        name: "SkiStyles",
                        styles: null,
                    },
                    SkiStylesResponse: {
                        name: "SkiStylesResponse",
                        styles: null,
                    }
                },
            }
        }
    },
}