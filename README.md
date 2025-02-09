Postech 4NETT Hackathon
Documentação elaborada para o microsserviço de autenticação desenvolvido no Hackathon.

📌 Visão Geral
Este repositório contém a implementação de um microsserviço para autenticação de outros microsserviços e gerenciamento de usuários, desenvolvido em .NET 8.0 e implantado em um ambiente Kubernetes. O projeto faz parte de um desafio de arquitetura de microsserviços, com foco na escalabilidade e resiliência.

🚀 Tecnologias Utilizadas
.NET 8.0 – Backend para o microsserviço de agendamentos.
Kubernetes – Orquestração dos serviços.
MongoDb – Banco de dados.
📂 Estrutura do Projeto
Postech-4NETT-Hackathon/
│-- kubernetes/                  # Manifests para deployment no Kubernetes
│   │-- hackathon-autenticacao-deployment.yaml
│   │-- mongo-deployment.yaml
│-- src/
│   │-- Postech.Hackathon.Autenticacao.Api/  # Código do microsserviço
│-- tests/                        # Testes unitários
│-- README.md                     # Este documento
🏗️ Deploy no Kubernetes
Criar os recursos necessários:
kubectl apply -f kubernetes/mongo-deployment.yaml
kubectl apply -f kubernetes/hackathon-autenticacao-deployment.yaml
Verificar os pods em execução:
kubectl get pods
Acessar os logs da API:
kubectl logs -f <nome-do-pod>
🔗 Testando a API
Deixamos o swagger hábilitado para realização de testes do endpoint com maior facilidade.

📜 Arquitetura e outras informações
A arquitetura completa da solução está disponível no Miro.

Esse trabalho foi planejado e implementado pelos desenvolvedores: Breno Jhefferson Gomes, Lucas Ruiz Dirani,  Lucas Montarroyos, Ricardo Fulgencio Alves, Tatiana Lima.
Os outros microsserviços que fazem parte da solução se encontra nos diretórios:"

Microsserviço de agendamento - Lucas Ruiz Dirani: https://github.com/lucasdirani/Postech-4NETT-Hackathon
Microsserviço de médicos - Tatiana Lima: https://github.com/tatianacardosolima/fiap.hackathon.api
Microsserviço de Pacientes - Lucas Montarroyos: https://github.com/lucasmrpinho/Postech-4NETT-Hackathon
