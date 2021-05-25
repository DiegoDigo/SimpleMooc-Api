run:
	dotnet run --project=SimpleMooc.Api

migrate:
	dotnet ef migrations add ${version} --project=SimpleMooc.Infra

update:
	dotnet ef database update  --project=SimpleMooc.Infra

db:
	docker-compose up -d
	
variaveis:
	cat secrets.json | dotnet user-secrets set --project=SimpleMooc.Api
