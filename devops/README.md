# NLW Unite Devops

> Evento da RocketSeat

## Terraform

### Comandos

- terraform init
- terraform fmt
- terraform plan
- terraform apply -auto-approve
- terraform plan --destroy

### Links úteis

- [Terraform Registry](https://registry.terraform.io/)
- [Terraform digitalocean_database_cluster](https://registry.terraform.io/providers/digitalocean/digitalocean/latest/docs/resources/database_cluster)
- [digitalocean_database_db](https://registry.terraform.io/providers/digitalocean/digitalocean/latest/docs/resources/database_db) <-

## Kubernetes (k8s)

### Comandos

- k3d cluster create nlw-unite --servers 2
- kubectl cluster-info

- kubectl get nodes
- kubectl get pods
- kubectl get pods -n nlw
- kubectl get pods -n kube-system

- kubectl create ns test
- kubectl get pods -n test
- kubectl apply -f k8s-example-with-nginx/deployment.yaml -n test
- kubectl get deployment -n test
- kubectl get pods -n test
- kubectl get replicaset -n test
- kubectl port-forward pod/[pod-name] -n test 3333:80
- kubectl apply -f k8s-example-with-nginx -n test
- kubectl get service -n test
- kubectl port-forward svc/nginx-service -n test 3333:80

- kubectl create ns nlw
- kubectl apply -f k8s -n nlw
- kubectl get deployment -n nlw
- watch kubectl get pods -n nlw
- kubectl logs [pod-name] -n nlw
- kubectl top pods -n nlw
- kubectl top nodes -n nlw
- kubectl port-forward svc/passin-service -n test 3333:3001
- kubectl apply -f k8s -n nlw
- kubectl get pods -n nlw
