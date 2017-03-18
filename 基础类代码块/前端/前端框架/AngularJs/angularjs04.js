/**
 * Created by wangyuhang on 2016/8/23.
 */
angular.module('app',[])
.controller('FirstCtrl',function ($scope) {
    $scope.msg='hello angular';
})
.controller('NextCtrl',function ($scope) {
    $scope.msg='hello angular2';
})