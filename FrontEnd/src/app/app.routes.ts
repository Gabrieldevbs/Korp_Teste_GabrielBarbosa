import { Routes } from '@angular/router';
import { Home } from './components/home/home';
import { Produtos } from './components/produtos/produtos';
import { EditarProduto } from './components/editar-produto/editar-produto';
import { NotasFiscais } from './components/notas-fiscais/notas-fiscais';
import { CriarProduto } from './components/criar-produto/criar-produto';
import { DetalhesNota } from './components/detalhes-nota/detalhes-nota';
import { VincularProduto } from './components/vincular-produto/vincular-produto';

export const routes: Routes = [
    {
        path: '',
        component: Home
    },
    {
        path: 'produtos',
        component: Produtos
    },
    {
        path: 'produtos/criar',
        component: CriarProduto
    },
    {
        path: 'produtos/:id',
        component: EditarProduto
    },
    {
        path: 'notasfiscais',
        component: NotasFiscais
    },
    {
        path: 'notasfiscais/detalhes/:id',
        component: DetalhesNota
    },
    {
        path: 'notasfiscais/adicionar/:id',
        component: VincularProduto
    },
];
