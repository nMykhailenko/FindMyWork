dotnet ef migrations add initial --project .\src\modules\files\FindMyWork.Modules.Files.Core --startup-project .\src\api\FindMyWork.Modular.API --output-dir Infrastructure\Persistence\Migrations


dotnet ef migrations add initial --project .\src\modules\jobs\FindMyWork.Modules.Jobs.Core --startup-project .\src\api\FindMyWorkModular.API --output-dir Infrastructure\Persistence\Migrations
