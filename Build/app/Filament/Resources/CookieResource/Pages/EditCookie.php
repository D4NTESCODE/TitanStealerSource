<?php

namespace App\Filament\Resources\CookieResource\Pages;

use App\Filament\Resources\CookieResource;
use Filament\Pages\Actions;
use Filament\Resources\Pages\EditRecord;

class EditCookie extends EditRecord
{
    protected static string $resource = CookieResource::class;

    protected function getActions(): array
    {
        return [
            Actions\DeleteAction::make(),
        ];
    }
}
