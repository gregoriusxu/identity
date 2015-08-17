(function () {
    var controllerId = 'app.views.menues.createDialog';
    angular.module('app').controller(controllerId, [
        'abp.services.app.menu', '$modalInstance', 'id', 'isview',
    function (menuService, $modalInstance, id, viewname) {
        var vm = this;


        vm.loadMenu = function (id, viewname) {
            abp.ui.setBusy(
                null,
                menuService.getMenu({
                    id: id
                }).success(function (data) {
                    vm.menu = data.menu;
                    vm.menuParentId = vm.menu.parentId;
                    vm.menuParentName = vm.menu.name;
                    vm.isview = viewname === "查看";
                })
            );
        };

        vm.save = function () {
            vm.menu.parentId = vm.menuParentId;
            if (id) {
                menuService
                    .editMenu(vm.menu)
                    .success(function () {
                        $modalInstance.close();
                    });
            } else {
                menuService
                   .createMenu(vm.menu)
                   .success(function () {
                       $modalInstance.close();
                   });
            }
        };

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        vm.menuClick = function (id, name) {
            vm.menuParentId = id;
            vm.menuParentName = name;
        }

        vm.menues = menuService.getAllMenues().success(function (data) {
            vm.menues = data;
        });

        if (id)
            vm.loadMenu(id, viewname);


        vm.viewname = viewname ? viewname : "新建";
    }
    ]);
})();