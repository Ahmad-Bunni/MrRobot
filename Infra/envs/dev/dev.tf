terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=3.8.0"
    }
  }
  backend "azurerm" {
    resource_group_name  = "tfstate-rg"
    storage_account_name = "mrrobottfstate"
    container_name       = "tfstate"
    key                  = "terraform.tfstate"
  }
}

provider "azurerm" {
  features {}
}

module "main" {
  source           = "../../main"
  subscription_id  = "bf5578cd-db06-4368-a38b-98cae085a6bc"
  project_name     = "mrrobot"
  environment_name = "dev"
}
