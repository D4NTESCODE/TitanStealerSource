<?php

namespace App\Filament\Resources\UserProfileResource\Pages;

use App\Filament\Resources\UserProfileResource;
use Filament\Pages\Actions;
use Filament\Resources\Pages\EditRecord;

class EditUserProfile extends EditRecord
{
    protected static string $resource = UserProfileResource::class;

    protected function getActions(): array
    {
        return [
            Actions\DeleteAction::make(),
        ];
    }
}
