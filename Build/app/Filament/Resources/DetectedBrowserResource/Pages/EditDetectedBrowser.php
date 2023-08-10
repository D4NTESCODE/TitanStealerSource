<?php

namespace App\Filament\Resources\DetectedBrowserResource\Pages;

use App\Filament\Resources\DetectedBrowserResource;
use Filament\Pages\Actions;
use Filament\Resources\Pages\EditRecord;

class EditDetectedBrowser extends EditRecord
{
    protected static string $resource = DetectedBrowserResource::class;

    protected function getActions(): array
    {
        return [
            Actions\DeleteAction::make(),
        ];
    }
}
