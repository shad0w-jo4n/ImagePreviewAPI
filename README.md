# ImagePreviewAPI
One method API preview.

Written in C# (ASP.NET Core WebAPI).

## Building and Running
### .NET Core
#### Requirments
* .NET Core SDK
#### Steps
1. Download/clone repository.
2. Compile solution and run project.

    `dotnet run --configuration Release`
3. Go to [localhost:5000](http://localhost:5000).
### Docker
#### Requirments
* Docker Desktop (or Docker Toolbox).
> If you use Docker Toolbox, you need to setup virtual machine.
#### Steps
1. Download/clone repository.
2. Build and run with:

    `docker-compose up`
3. Go to [localhost](http://localhost).
> If you use Docker Toolbox, you need go to `http://ip-addr/`, where `ip-addr` is ip-address of virtual machine.

## API Documentation
This service has only one method.

**POST** /api/image/upload

Upload image/images by links.

### Input
Content-Type: application/json
> Another mime-types doesn't supported.
#### Parameters
* imagesUrls *Required*

Array of strings (URLs to images). Min count of array elements: 1
#### Example

    {
        "imagesUrls": [
            "first link",
            "second link"
        ]
    }
### Output
Content-Type: application/json
> Another mime-types doesn't supported.
#### Parameters
* uploaded

Object. Key is link. Value is object with `original` and `preview` properties.
> `original`
>> String. Link to uploaded original image.

> `preview`
>> String. Link to uploaded 100px x 100px image.
* failed

Array of strings (links, which images doesn't uploaded)
#### Example

    {
        "uploaded": {
            "first link": {
                "original": "{link_to_image}",
                "preview": "{link_to_image}"
            },
            "third link": {
                "original": "{link_to_image}",
                "preview": "{link_to_image}"
            },
            "fifth link": {
                "original": "{link_to_image}",
                "preview": "{link_to_image}"
            }
        },
        "failed": [
            "second link",
            "fourth link"
        ]
    }
