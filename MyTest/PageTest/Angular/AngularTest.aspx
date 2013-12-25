<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AngularTest.aspx.cs" Inherits="MyTest.PageTest.Angular.AngularTest" %>
<!doctype html>
<html ng-app>
    <head>
        <script src="http://code.angularjs.org/angular-1.0.1.min.js"></script>
    </head>
    <body>
        Hello {{'World'}}!
        <div id="divTest" ng-controller="TestCtrl" ng-click="click()">{{a}}</div>
        <input type="text" id="txtTest" ng-control="TextCtrl" ng-model="b" />

        <script type="text/javascript" charset="utf-8">

            var TestCtrl = function ($scope) {

                $scope.a = "123";
                $scope.click = function () {
                    $scope.a = "abc";
                }
            }

            var TextCtrl = function ($scope) {
                $scope.b = "123";
                alert($("#txtTest").scope().b);
            }
           
            angular.bootstrap(document.documentElement);
        </script>


    </body>
</html>