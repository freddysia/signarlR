(function () {
    'use strict';
    var app = angular.module('liveTradingApp');
    var $Jquery = $;


    function LiveTradingController($scope, $http, $q) {
        var vm = this;
        var dataLoaded = false;
        var GidDataSource;
        vm.AsOfDate = new Date().toLocaleDateString();

        var historyTradesArray = [];
        function convertMessageStringToDataSource(str) {
            //var obj = JSON.parse(str);
            var dataSource = new Object();
            dataSource.XRef = str;
            dataSource.XRefType = "A-trade";
            dataSource.Identifier = str;
            dataSource.ProductType = str;
            dataSource.TradeID = new Date().toLocaleDateString();
            return dataSource;
        }

        
        $Jquery.connection.hub.url = "http://localhost:8632/signalr";
        //var liveTradingHub = $Jquery.connection.moveShapeHub;
        var liveTradingHub = $Jquery.connection.liveTradingHub;
        //Start
        $Jquery.connection.hub.start().done(function () {
            liveTradingHub.server.sendMqMessage("name: Fred");
        });
        //liveTradingHub.client.addMessage = function (name, message) {
        //    alert(name + message);
        //}
        liveTradingHub.client.sendMessage = function (message) {
            historyTradesArray.push(message);
            if (GidDataSource != undefined) {
                if (dataLoaded) {
                    GidDataSource.insert(0, convertMessageStringToDataSource(message));
                }
                else {
                    for (var i = 0; i < historyTradesArray.length; i++) {
                        GidDataSource.insert(0, convertMessageStringToDataSource(historyTradesArray[i]));
                    }
                    dataLoaded = !dataLoaded;
                }
            }
        };

        



        setTimeout(function () {
            GidDataSource = new kendo.data.DataSource({
                transport: {
                    read: "trade.xml"
                    //read: function (options) {
                    //    $http({
                    //        method: 'POST',
                    //        url: "http://spiritappua.nam.nsroot.net:8625/spirit/xmlpost/TradeService/GetTrades",
                    //        data: requestData,
                    //        headers: {
                    //            "Content-Type": "text/xml"
                    //        }
                    //    }).then(function (response) {
                    //        options.success(response);
                    //    });
                    //},
                },
                schema: {
                    // specify the the schema is XML
                    type: "xml",
                    data: "/Trades/Trade",
                    model: {
                        fields: {
                            XRef: "XRef/text()",
                            XRefType: "XRefType/text()",
                            Identifier: "Identifier/text()",
                            ProductType: "ProductType/text()",
                            TradeID: "TradeID/text()"
                        }
                    }
                },
                pageSize: 50
            });
            vm.tradeGrid.setOptions({
                dataSource: GidDataSource,
            });
        }, 0)

        vm.callTradeService = function () {
            getTradeService().then(function (response) {
                console.log(response.data);
            })
        };

        vm.tradeOptions = {
            sortable: true,
            resizable: true,
            columns: [
				{ "title": "XRef", "field": "XRef", "width": 115 },
				{ "title": "XRefType", "field": "XRefType", "width": 150 },
				{ "title": "Identifier", "field": "Identifier", "width": 105 },
				{ "title": "ProductType", "field": "ProductType", "width": 150 },
				{ "title": "TradeID", "field": "TradeID", "width": 134 }
            ],
            columnMenu: true,
            reorderable: true,
            filterable: true,
            pageable: true,
            excel: {
                fileName: "liveTrading.xlsx",
                proxyURL: "",
                filterable: true
            },
            pageSize: 25,
            height: 600,
        };

















        var getTradeService = function () {
            var requestData = "<GetTrades>" + "<accountCriteriaType>" + "AccountGroup" + "</accountCriteriaType>" +

	"<accountCriteria>" + "<item>" + "AGY.CMO" + "</item>" +

	"</accountCriteria>" + "<tradeDate>" + 20180409 + "</tradeDate>" +

"</GetTrades>";
            return $q(function (resolve, reject) {
                $http({
                    method: 'POST',
                    url: "http://spiritappua.nam.nsroot.net:8625/spirit/xmlpost/TradeService/GetTrades",
                    data: requestData,
                    headers: {
                        "Content-Type": "text/xml"
                    }
                }).then(function (response) {
                    resolve(response);
                }, reject)
            })
        };

        return vm;
    }

    app.controller('LiveTradingController', LiveTradingController);
})();

$(function () {

});