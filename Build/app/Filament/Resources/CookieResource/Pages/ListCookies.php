<?php

namespace App\Filament\Resources\CookieResource\Pages;

use App\Filament\Resources\CookieResource;
use Filament\Pages\Actions;
use Filament\Resources\Pages\ListRecords;

class ListCookies extends ListRecords
{
    protected static string $resource = CookieResource::class;

    protected function getActions(): array
    {
        return [
            //Actions\CreateAction::make(),
        ];
    }
}
