<?php

namespace App\Filament\Resources;

use App\Filament\Resources\HwidResource\Pages;
use App\Filament\Resources\HwidResource\RelationManagers;
use App\Models\Hwid;
use Filament\Forms;
use Filament\Resources\Form;
use Filament\Resources\Resource;
use Filament\Resources\Table;
use Filament\Tables;
use Illuminate\Database\Eloquent\Builder;
use Illuminate\Database\Eloquent\SoftDeletingScope;
use Filament\Tables\Actions\Action;

class HwidResource extends Resource
{
    protected static ?string $model = Hwid::class;
    protected static ?string $navigationGroup = 'General';
    protected static ?string $navigationIcon = 'heroicon-o-collection';

    public static function form(Form $form): Form
    {
        return $form
            ->schema([

            ]);
    }

    public static function table(Table $table): Table
    {
        return $table
            ->columns([
                Tables\Columns\TextColumn::make('hash'),
                Tables\Columns\TextColumn::make('browsers_count')->counts('browsers')
            ])
            ->filters([
                //
            ])
            ->actions([
                //Tables\Actions\EditAction::make(),
            ])
            ->bulkActions([
                Tables\Actions\DeleteBulkAction::make(),
            ]);
    }

    public static function getRelations(): array
    {
        return [
            //
        ];
    }

    public static function getPages(): array
    {
        return [
            'index' => Pages\ListHwids::route('/'),
            'create' => Pages\CreateHwid::route('/create'),
            'edit' => Pages\EditHwid::route('/{record}/edit'),
        ];
    }
}
