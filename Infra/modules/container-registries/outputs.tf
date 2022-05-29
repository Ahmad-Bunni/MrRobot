output "mrrobot_container_registery_id" {
  value       = azurerm_container_registry.mrrobot-acr.id
  description = "The id of the api container registery"
}
output "mrrobot_container_registery_password" {
  value       = azurerm_container_registry.mrrobot-acr.admin_password
  description = "The passowrd of the api container registery"
  sensitive   = true
}
output "mrrobot_container_registery_username" {
  value       = azurerm_container_registry.mrrobot-acr.mrrobot_container_registery_username
  description = "The username of the api container registery"
  sensitive   = true
}
output "mrrobot_container_registery_server" {
  value       = azurerm_container_registry.mrrobot-acr.login_server
  description = "The server of the api container registery"
  sensitive   = true
}
