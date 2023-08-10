<?php

namespace App\Filament\Resources\UserProfileResource\Pages;

use App\Filament\Resources\UserProfileResource;
use Filament\Pages\Actions;
use Filament\Resources\Pages\CreateRecord;

class CreateUserProfile extends CreateRecord
{
    protected static string $resource = UserProfileResource::class;
}
