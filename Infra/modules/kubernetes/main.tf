resource "azurerm_kubernetes_cluster" "mrrobot-k8s" {
  name                = "aks-cluster-${var.project_name}-${var.environment_name}"
  resource_group_name = var.resource_group_name
  location            = var.resource_group_location
  dns_prefix          = local.dns_prefix

  default_node_pool {
    name       = "default"
    node_count = 1
    vm_size    = "Standard_B2s"
  }

  identity {
    type = "SystemAssigned"
  }

  tags = {
    Environment = var.environment_name
  }
}
