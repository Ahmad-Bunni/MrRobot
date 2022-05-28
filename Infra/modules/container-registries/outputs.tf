output "mrrobot_container_registery_id" {
  value       = azurerm_container_registry.mrrobot-acr.id
  description = "The id of the api container registery"
}
output "mrrobot_container_registery_password" {
  value       = azurerm_container_registry.mrrobot-acr.admin_password
  description = "The passowrd of the api container registery"
  sensitive   = true
}
