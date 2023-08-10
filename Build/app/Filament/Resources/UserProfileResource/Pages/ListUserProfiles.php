<?php

namespace App\Filament\Resources\UserProfileResource\Pages;

use App\Filament\Resources\UserProfileResource;
use Filament\Pages\Actions;
use Filament\Resources\Pages\ListRecords;

class ListUserProfiles extends ListRecords
{
    protected static string $resource = UserProfileResource::class;

    protected function getActions(): array
    {
        return [
           // Actions\CreateAction::make(),
        ];
    }
}
