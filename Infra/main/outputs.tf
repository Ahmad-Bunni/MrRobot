output "kube_config_raw" {
  value       = module.kubernetes.kube_config_raw
  description = "kubernetes cluster raw kube conifg"
  sensitive   = true
}
