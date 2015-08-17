(function () {
    var controllerId = 'app.views.menues.index';
    angular.module('app').controller(controllerId, [
        'abp.services.app.menu', '$modal',
        function (menuesService, $modal) {
            var vm = this;
            vm.permissions = {
                canCreateMenues: abp.auth.hasPermission("CanCreateMenues")
            };

            vm.sortingDirections = ['CreationTime DESC', 'Name DESC', 'DisplayName DESC', 'Order DESC'];

            vm.menues = [];
            vm.totalMenuCount = 0;
            vm.sorting = 'CreationTime DESC';

            vm.loadMenues = function (append) {
                var skipCount = append ? vm.menues.length : 0;
                abp.ui.setBusy(
                    null,
                    menuesService.getMenues({
                        skipCount: skipCount,
                        sorting: vm.sorting
                    }).success(function (data) {
                        if (append) {
                            for (var i = 0; i < data.items.length; i++) {
                                vm.menues.push(data.items[i]);
                            }
                        } else {
                            vm.menues = data.items;
                        }

                        vm.totalMenuCount = data.totalCount;
                    }).error(function (data, status, headers, config) {
                        abp.log.error(data);
                        // called asynchronously if an error occurs
                        // or server returns response with an error status.
                    })
                );
            };

            vm.showNewMenuDialog = function (id, isview) {
                var modalInstance = $modal.open({
                    templateUrl: abp.appPath + 'App/Main/views/menues/createDialog.cshtml',
                    controller: 'app.views.menues.createDialog as vm',
                    size: 'md',
                    resolve: {
                        id: function () { return id; },
                        isview: function () { return isview; }
                    }
                });

                modalInstance.result.then(function () {
                    vm.loadMenues();
                });
            };

            vm.sort = function (sortingDirection) {
                vm.sorting = sortingDirection;
                vm.loadMenues();
            };

            vm.showMore = function () {
                vm.loadMenues(true);
            };

            vm.deleteMenu = function (id) {
                if (confirm("确认要删除菜单吗？"))
                    menuesService.deleteMenu({ id: id }).success(
                        function (data) {
                            vm.loadMenues();
                        }
                    );
            }

            vm.loadMenues();
        }
    ]);
})();