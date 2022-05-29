output "kube_config_raw" {
  value       = module.main.kube_config_raw
  description = "kubernetes cluster raw kube conifg"
  sensitive   = true
}
