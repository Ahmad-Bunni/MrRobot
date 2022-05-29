output "kube_config_raw" {
  value       = module.kubernetes.kube_config_raw
  description = "kubernetes cluster raw kube conifg"
  sensitive   = true
}
output "mrrobot_container_registery_password" {
  value       = module.container_registries.mrrobot-acr.admin_password
  description = "The passowrd of the api container registery"
  sensitive   = true
}
output "mrrobot_container_registery_username" {
  value       = module.container_registries.mrrobot-acr.mrrobot_container_registery_username
  description = "The username of the api container registery"
  sensitive   = true
}
output "mrrobot_container_registery_server" {
  value       = module.container_registries.mrrobot-acr.login_server
  description = "The server of the api container registery"
  sensitive   = true
}
