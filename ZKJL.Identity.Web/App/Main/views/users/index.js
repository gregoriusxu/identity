(function () {
    var controllerId = 'app.views.users.index';
    angular.module('app').controller(controllerId, [
        'abp.services.app.user', '$modal',
        function (userService, $modal) {
            var vm = this;

            vm.permissions = {
                canCreateUsers: abp.auth.hasPermission("CanCreateUsers")
            };

            vm.sortingDirections = ['CreationTime DESC', 'Name DESC', 'UserName DESC', 'Surname DESC'];

            vm.users = [];
            vm.sorting = 'CreationTime DESC';

            vm.showNewUserDialog = function (id, isview) {
                var modalInstance = $modal.open({
                    templateUrl: abp.appPath + 'App/Main/views/users/createDialog.cshtml',
                    controller: 'app.views.users.createDialog as vm',
                    size: 'md',
                    resolve: {
                        id: function () { return id; },
                        isview: function () { return isview; }
                    }
                });

                modalInstance.result.then(function () {
                    vm.loadUsers();
                });
            };

            vm.sort = function (sortingDirection) {
                vm.sorting = sortingDirection;
                vm.loadUsers();
            };

            vm.showMore = function () {
                vm.loadUsers(true);
            };

            vm.deleteUser = function (id) {
                if (confirm("确认要删除用户吗？"))
                    userService.deleteUser({ id: id }).success(
                        function (data) {
                            vm.loadUsers();
                        }
                    );
            }

            vm.loadUsers = function (append) {
                var skipCount = append ? vm.menues.length : 0;
                abp.ui.setBusy(
                      null,
                      userService.getUsers({
                          skipCount: skipCount,
                          sorting: vm.sorting
                      }).success(function (data) {
                          vm.users = data.items;
                      }).error(function (data, status, headers, config) {
                          abp.log.error(data);
                          // called asynchronously if an error occurs
                          // or server returns response with an error status.
                      })
                  );
            }

            vm.loadUsers();

        }
    ]);
})();