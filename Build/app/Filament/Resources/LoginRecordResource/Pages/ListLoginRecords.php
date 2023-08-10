<?php

namespace App\Filament\Resources\LoginRecordResource\Pages;

use App\Filament\Resources\LoginRecordResource;
use Filament\Pages\Actions;
use Filament\Resources\Pages\ListRecords;

class ListLoginRecords extends ListRecords
{
    protected static string $resource = LoginRecordResource::class;

    protected function getActions(): array
    {
        return [
            //Actions\CreateAction::make(),
        ];
    }
}
