# Бизнес задача
По умолчанию имеется список из 5 городов (Москва, Cанкт-Петербург, Нижний Новгород, Новосибирск, Екатеринбург).
У каждого города, есть список из 100 ресторанов.
Система имеет следующий функционал:
- GET Метод /api/rests-and-cities/get-cities-list - Получение списка городов
- POST Метод /api/rests-and-cities/restaurants-list - Получение списка ресторанов по определённому городу
- POST Метод /api/rests-and-cities/add-city - Добавление города
- DELETE Метод /api/rests-and-cities/remove-city/{cityId} - Удаление города
- POST Метод /api/rests-and-cities/edit-city/{cityId} - Изменение города
- POST Метод /api/rests-and-cities/{cityId}/add-restaurant - Добавление ресторана
- DELETE Метод /api/rests-and-cities/remove-restaurant/{restaurantId} - Удаление ресторана
- POST Метод /api/rests-and-cities/edit-restaurant/{restaurantId} - Изменение ресторана

# Предварительная настройка
1. Необходимо наличие .NET 5.0 SDK
2. Скачать можно тут: https://dotnet.microsoft.com/en-us/download/dotnet/5.0

# I. Компиляция proto
1. Перейти в каталог \TestTask\Protos с Protos.csproj
2. Выполнить в терминале:
- dotnet restore
- dotnet publish -c Release -o ./release

# II. Компиляция и запуск сервиса GrpcService
1. Перейти в каталог \TestTask\GrpcService с GrpcService.csproj
2. Выполнить в терминале:
- dotnet restore
- dotnet publish -c Release -o ./release
- cd release
- dotnet GrpcService.dll

# III. Компиляция и запуск сервиса WebApiService
1. Перейти в каталог \TestTask\WebApiService с WebApiService.csproj
2. Выполнить в терминале:
- dotnet restore
- dotnet publish -c Release -o ./release
- cd release
- dotnet WebApiService.dll

# IV. Тестирование сервисов

## Тестирование сервиса gRPC-сервиса (GrpcService)
1. Необхимо скачать утилиту grpcui (https://github.com/fullstorydev/grpcui/releases)
2. Перейти в каталог где раполагается grpcui.exe
3. Выполнить в терминале:
- rpcui.exe -plaintext localhost:5001
4. Откроется окно браузера с интерактивным пользовательским веб-интерфейсом
5. Подробнее про тестирование gRPC-сервисов тут: 
https://docs.microsoft.com/ru-ru/aspnet/core/grpc/test-tools?view=aspnetcore-6.0
(Тестирование служб gRPC с помощью gRPCurl в ASP.NET Core)

## Тестирование WebApi-сервиса + gRPC-сервиса (WebApiService + GrpcService)
1. Перейти по адресу http://localhost:5000/swagger