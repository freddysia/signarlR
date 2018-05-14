(function () {
    'use strict';
    angular.module('liveTradingApp', ["kendo.directives", "ngRoute", "ngAnimate"])
        .config(['$httpProvider', function ($httpProvider) {
            $httpProvider.defaults.cache = true;
        }])
        //.run(function ($window, MetaService) {
        //    MetaService.GetEnviorment()
        //        .then(function (data) {
        //            if (data && data.data && data.data.Environment) {
        //                $window.document.title = data.data.Environment + '-' + $window.document.title;
        //            }
        //        });
        //});
}());