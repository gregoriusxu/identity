(function () {
    var controllerId = 'app.views.users.createDialog';
    angular.module('app').controller(controllerId, [
        'abp.services.app.user', 'abp.services.app.multiTenancy', '$modalInstance', 'id', 'isview',
    function (userService, multiTenancyservice, $modalInstance, id, viewname) {
        var vm = this;


        vm.loadUser = function (id, viewname) {
            abp.ui.setBusy(
                null,
                userService.getUser({
                    id: id
                }).success(function (data) {
                    vm.user = data.user;
                    vm.tenantId = vm.user.tenantId;
                    vm.tenantName = vm.user.tenant.name;
                    vm.isview = viewname === "查看";
                })
            );
        };

        vm.loadMultiTenancy = function () {
            multiTenancyservice.getMultiTenancys().success(function (data) {
                vm.multiTenancys = data;
            }).error(function (data, status, headers, config) {
                abp.log.error(data);
                // called asynchronously if an error occurs
                // or server returns response with an error status.
            });
        };

        vm.tenantClick = function (id, name) {
            vm.tenantId = id;
            vm.tenantName = name;
        }

        vm.save = function () {
            vm.user.tenantId = vm.tenantId;
            if (id) {
                userService
                    .editUser(vm.user)
                    .success(function () {
                        $modalInstance.close();
                    });
            } else {
                userService
                 .createUser(vm.user)
                 .success(function () {
                     $modalInstance.close();
                 });
            }

        };


        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        vm.loadMultiTenancy();

        if (id)
            vm.loadUser(id, viewname);


        vm.viewname = viewname ? viewname : "新建";
    }
    ]);
})();