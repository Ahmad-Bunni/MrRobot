resource "azurerm_container_registry" "mrrobot-acr" {
  name                = "acr${var.project_name}${var.environment_name}"
  resource_group_name = var.resource_group_name
  location            = var.resource_group_location
  sku                 = "Basic"
  admin_enabled       = false
}
