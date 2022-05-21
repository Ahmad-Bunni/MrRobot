module "resource_groups" {
  source           = "../modules/resource-groups"
  project_name     = var.project_name
  environment_name = var.environment_name
}


module "kubernetes" {
  source                  = "../modules/kubernetes"
  project_name            = var.project_name
  environment_name        = var.environment_name
  resource_group_name     = module.resource_groups.mrrobot_rg_name
  resource_group_location = module.resource_groups.mrrobot_rg_location
}

module "container_registries" {
  source                  = "../modules/container-registries"
  project_name            = var.project_name
  environment_name        = var.environment_name
  resource_group_name     = module.resource_groups.mrrobot_rg_name
  resource_group_location = module.resource_groups.mrrobot_rg_location
}
