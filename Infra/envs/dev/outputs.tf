output "kube_config_raw" {
  value       = module.main.kube_config_raw
  description = "kubernetes cluster raw kube conifg"
  sensitive   = true
}
output "mrrobot_container_registery_password" {
  value       = module.main.mrrobot-acr.admin_password
  description = "The passowrd of the api container registery"
  sensitive   = true
}
output "mrrobot_container_registery_username" {
  value       = module.main.mrrobot-acr.mrrobot_container_registery_username
  description = "The username of the api container registery"
  sensitive   = true
}
output "mrrobot_container_registery_server" {
  value       = module.main.mrrobot-acr.login_server
  description = "The server of the api container registery"
  sensitive   = true
}
