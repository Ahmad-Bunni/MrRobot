output "mrrobot_rg_name" {
  value       = azurerm_resource_group.mrrobot_rg.name
  description = "The name of the Auto Scaling Group"
}

output "mrrobot_rg_location" {
  value       = azurerm_resource_group.mrrobot_rg.location
  description = "The name of resource group location"
}
