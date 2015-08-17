(function () {
    'use strict';

    var app = angular.module('app', [
        'ngAnimate',
        'ngSanitize',
        'ngMaterial',

        'ui.router',
        'ui.bootstrap',
        'ui.jq',

        'abp'
    ]);

    //Configuration for Angular UI routing.
    app.config([
        '$stateProvider', '$urlRouterProvider',
        function ($stateProvider, $urlRouterProvider) {
            $urlRouterProvider.otherwise('/menues');
            $stateProvider
                .state('menues', {
                    url: '/menues',
                    templateUrl: abp.appPath + 'App/Main/views/menues/index.cshtml',
                    menu: 'menues' //Matches to name of 'Menues' menu in IdentityNavigationProvider
                })
                .state('roles', {
                    url: '/roles',
                    templateUrl: abp.appPath + 'App/Main/views/roles/index.cshtml',
                    menu: 'Roles' //Matches to name of 'Roles' menu in IdentityNavigationProvider
                })
                .state('rolemenues', {
                    url: '/roles/:id',
                    templateUrl: abp.appPath + 'App/Main/views/roles/detail.cshtml',
                    menu: 'Roles' //Matches to name of 'Roles' menu in IdentityNavigationProvider
                })
                .state('users', {
                    url: '/users',
                    templateUrl: abp.appPath + 'App/Main/views/users/index.cshtml',
                    menu: 'Users' //Matches to name of 'Users' menu in IdentityNavigationProvider
                })
                .state('userroles', {
                    url: '/users/:id',
                    templateUrl: abp.appPath + 'App/Main/views/users/detail.cshtml',
                    menu: 'Users' //Matches to name of 'Users' menu in IdentityNavigationProvider
                });
        }
    ]);
})();