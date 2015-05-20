using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Before.Infrastructure.Identity
{
    public class RoleListItemVm
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
    }
    public class GroupListItemVm
    {
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
    }
    public class GroupCreateVm
    {
        public CreateGroupDto ToDto()
        {
            return new CreateGroupDto();
        }
    }
    public class GroupDeleteVm
    {
        public DeleteGroupDto ToDto()
        {
            return new DeleteGroupDto();
        }
    }
    public class GroupUpdateVm
    {
        public UpdateGroupDto ToDto()
        {
            return new UpdateGroupDto();
        }
    }
    public class RoleCreateVm
    {
        public CreateRoleDto ToDto()
        {
            return new CreateRoleDto();
        }
    }
    public class RoleDeleteVm
    {
        public DeleteRoleDto ToDto()
        {
            return new DeleteRoleDto();
        }
    }
    public class RoleUpdateVm
    {
        public UpdateRoleDto ToDto()
        {
            return new UpdateRoleDto();
        }
    }
    public class CreateGroupDto { }
    public class DeleteGroupDto { }
    public class UpdateGroupDto { }
    public class CreateRoleDto { }
    public class DeleteRoleDto { }
    public class UpdateRoleDto { }
}
