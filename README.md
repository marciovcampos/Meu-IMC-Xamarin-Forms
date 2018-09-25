# CalculadoraFreelancer06

Se trata da continuação do app <a href="https://github.com/dayaneLima/CalculadoraFreelancer05">CalculadoraFreelancer05</a>

## Exibindo nossos projetos

Vamos agora exibir a listagem dos nossos projetos.

### Alteração do Service ProjetoService

Primeiramente vamos alterar o nosso ProjetoService para adicionarmos a função de trazer todos os nossos projetos. Primeiramente edite o arquivo chamado IProjetoService.cs, ele se encontra no projeto CalcFreelancer, dentro da pasta Services -> Interfaces. Vamos adicionar a assinatura para retornar uma listagem de projetos. 

```c#
public interface IProjetoService
{
	void Inserir(Projeto projeto);
	Task<IEnumerable<Projeto>> ObterTodos();
}
````

Agora vamos no nosso ProjetoService e implementar essa função. Edite o arquivo ProjetoService.cs, que se encontra no projeto CalcFreelancer na pasta Services. O que precisamos fazer é chamar a função GetAll da nossa ProjetoRepository:

```c#
...

public Task<IEnumerable<Projeto>> ObterTodos()
{
    return ProjetoRepository.GetAll();
}

...

````

Nosso service ficou assim:

```c#
public class ProjetoService: IProjetoService
{
	private readonly IProjetoRepository ProjetoRepository;

	public ProjetoService(IProjetoRepository projetoRepository)
	{
	    ProjetoRepository = projetoRepository;
	}

	public void Inserir(Projeto projeto)
	{
	    ProjetoRepository.Insert(projeto);
	}

	public Task<IEnumerable<Projeto>> ObterTodos()
	{
	    return ProjetoRepository.GetAll();
	}
}
````

### Criação da ViewModel ProjetosPageViewModel

Agora dentro da pasta ViewModels vamos criar uma classe chamada ProjetosPageViewModel. Vá em Add -> Class e dê o nome de ProjetosPageViewModel.

Vamos herdar da nossa ViewModelBase e colocar a classe como pública:

```c#
public class ProjetosPageViewModel : ViewModelBase
{
}
````

Vamos agora receber por injeção de dependência o objeto do tipo IProjetoService, como vimos na aula anterior:

```c#
public class ProjetosPageViewModel : ViewModelBase
{
	private readonly IProjetoService ProjetoService;

	public ProjetosPageViewModel(IProjetoService projetoService)
	{
	    ProjetoService = projetoService;
	}
}
````

Vamos agora criar uma propriedade para ser a nossa lista de projetos que exibiremos na tela. Vamos criar uma propriedade não tipo List, mas sim do tipo ObservableCollection, desta forma:

```c#
public ObservableCollection<Projeto> Projetos { get; set; }
````

A diferença do ObservableCollection é que o mesmo implementa de INotifyCollectionChanged e INotifyPropertyChanged, ou seja, caso a nossa lista sofra alterações (adicionando ou removento um item da lista, por exemplo), a nossa tela será notificada. 

No construtor da nossa ViewModel vamos instanciar a nossa lista, ficando assim:

```c#
public class ProjetosPageViewModel : ViewModelBase
{
	private readonly IProjetoService ProjetoService;

	public ObservableCollection<Projeto> Projetos { get; set; }

	public ProjetosPageViewModel(IProjetoService projetoService)
	{
	    ProjetoService = projetoService;
	    Projetos = new ObservableCollection<Projeto>();
	}
}
````

Agora vamos criar uma função chamada ObterProjetos. Nesta função primeiramente vamos chamar o nosso service para obter a lista dos projetos:


```c#

... 
private async Task ObterProjetos()
{
	var projetos = await ProjetoService.ObterTodos();
}

...

````

Agora vamos ver se a nossa lista de Projetos está preenchida, se tiver, vamos limpá-la para inserir os novos registros:

```c#
... 
private async Task ObterProjetos()
{
	var projetos = await ProjetoService.ObterTodos();

	if (Projetos.Count > 0)
	{
		Projetos.Clear();
	}

}

...

````

Feito isso, vamos percorrer os itens obtidos e adicionar na nossa lista Projetos:


```c#
... 
private async Task ObterProjetos()
{
	var projetos = await ProjetoService.ObterTodos();

	if (Projetos.Count > 0)
	{
		Projetos.Clear();
	}

	foreach (var projeto in projetos)
	{
		Projetos.Add(projeto);
	}
}

...

````

No construtor da nossa classe ProjetosPageViewModel vamos chamar esta função que acabamos de criar, para que assim que a tela abra a lista já seja carregada:

```c#
...

public ProjetosPageViewModel(IProjetoService projetoService)
{
    ProjetoService = projetoService;
    Projetos = new ObservableCollection<Projeto>();
    ObterProjetos();
}

...
````

Em aplicativos é comum quando temos uma tela de listagem termos a funcionalidade de fazer o movimento de puxar para baixo e a tela atualizar, pensando nisso, vamos criar um  Command chamado de AtualizarDados, que a única coisa que irá fazer é chamar novamente a nossa função ObterProjetos. Vamos criar esse Command para a nossa View chamar quando o usuário fazer o movimento de atualização da tela. Temos que criar uma outra propriedade do tipo boleano para controlar se a lista já está sendo ou não atualizada, para evitar que o usuário fique toda hora clicando para atualizar tendo uma atualização em andamento. Vamos criar uma propriedade bindable chamada Atualizando, dessa forma:
```c#
...
	private bool atualizando;
	public bool Atualizando
	{
		get { return atualizando; }
		set { SetProperty(ref atualizando, value); }
	}

...
````

Agora vamos criar a propriedade do Command:

```c#
...
         public Command AtualizarDadosCommand { get; }        
...
````

Agora vamos iniciar essa propriedade no nosso construtor:

```c#
...

	public ProjetosPageViewModel(IProjetoService projetoService)
	{
	    ProjetoService = projetoService;
	    Projetos = new ObservableCollection<Projeto>();
	    ObterProjetos();
	    AtualizarDadosCommand  = new Command(ExecuteAtualizarDadosCommand );
	}

...
````

Vamos agora criar a função ExecuteAtualizarDados, antes dela chamar a ObterProjetos, vamos setar a variável Atualizando como true, e após chamarmos a função, vamos setá-la como false, informando que já terminou a atualização da lista, dessa forma:

```c#
...
private async void ExecuteAtualizarDadosCommand()
{
    Atualizando = true;
    await ObterProjetos();
    Atualizando = false;
}
...
````

Nossa viewModel ficou assim:

```c#
public class ProjetosPageViewModel : ViewModelBase
{
	private readonly IProjetoService ProjetoService;

	public ObservableCollection<Projeto> Projetos { get; set; }

	private bool atualizando;
	public bool Atualizando
	{
	    get { return atualizando; }
	    set { SetProperty(ref atualizando, value); }
	}

	public Command AtualizarDados { get; }        

	public ProjetosPageViewModel(IProjetoService projetoService)
	{
	    ProjetoService = projetoService;
	    Projetos = new ObservableCollection<Projeto>();
	    ObterProjetos();
	    AtualizarDados = new Command(ExecuteAtualizarDados);
	}

	private async void ExecuteAtualizarDados()
	{
	    Atualizando = true;
	    await ObterProjetos();
	    Atualizando = false;
	}

	private async Task ObterProjetos()
	{
	    var projetos = await ProjetoService.ObterTodos();

	    if (Projetos.Count > 0)
	    {
		Projetos.Clear();
	    }

	    foreach (var projeto in projetos)
	    {
		Projetos.Add(projeto);
	    }
	}
}
````

### Criação da View Projetos

Dentro do projeto CalcFreelancer, clique com o botão direito e vá em Add -> New Item, escolha na esquerda o Xamarin Forms, e a direita escolha o Content Page, dê o nome de ProjetosPage. Agora arraste o arquivo criado para a pasta Views.

 <img src="https://github.com/dayaneLima/CalculadoraFreelancer06/blob/master/Docs/Imgs/aula_6_add_view_projetos_page.png" alt="Criação da View ProjetosPage" width="100%">

  No CodeBehind vamos setar a nossa ViewModel ProjetosPageViewModel como BindingContext dessa View. Edite o arquivo ProjetosPage.xaml.cs e no construtor dessa classe adicione o BindingContext:
  
  ```c#
public partial class ProjetosPage : ContentPage
{
	public ProjetosPage ()
	{
		InitializeComponent ();
		var viewModel = ServiceLocator.Current.GetInstance<ProjetosPageViewModel>();
		BindingContext = viewModel;
	}
}
  ````
  
  Agora vamos na nossa View. Edite o arquivo chamado ProjetosPage.xaml e adicione um ListView e não esqueça de adicionar um Title para a página, no caso chamamos de Projetos:
  
````xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CalcFreelancer.ProjetosPage"
             Title="Projetos">
    <ContentPage.Content>
        <ListView>
        </ListView>
    </ContentPage.Content>
</ContentPage>
````
  
  O ListView tem a propriedade ItemsSource, no qual informamos qual a lista que será utilizada, no nosso caso será a Projetos da nossa ViewModel, necessitando utilizar o Binding, para informar que é uma variável Bindable.
  
  ````xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CalcFreelancer.ProjetosPage"
             Title="Projetos">
    <ContentPage.Content>
        <ListView ItemsSource="{Binding Projetos}">
        </ListView>
    </ContentPage.Content>
</ContentPage>
````
  
  Agora vamos habilitar a atualização da lista quando o usuário fizer o movimento de puxar para baixo, para isso atribua para o IsPullToRefreshEnabled o valor de True. 
  
  Também temos que informar qual o Command que deve ser executado quando o usuário fizer o movimento de atualizar, então atribua para a propriedade RefreshCommand o nome AtualizarDadosCommand, que é o nosso Command de atualização dos dados que criamos na ViewModel. 
  
  Temos também que informar se a lista está ou não sendo atualizada, para evitar que o usuário fique atualizando antes mesmo que uma atualização seja concluída, para isso atribua para a propriedade IsRefreshing a variável Atualizando que criamos na ViewModel. 
  
  Ficou dessa forma:
  
  ````xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CalcFreelancer.ProjetosPage"
             Title="Projetos">
    <ContentPage.Content>
        <ListView ItemsSource="{Binding Projetos}"
                  IsPullToRefreshEnabled="True"
                  RefreshCommand="{Binding AtualizarDadosCommand}"
                  IsRefreshing="{Binding Atualizando}">
        </ListView>
    </ContentPage.Content>
</ContentPage>
````

O ListView é preparado para ter somente uma linha de exibição para cada item da lista. Vamos exibir o nome do projeto e logo abaixo o valor, então teremos mais de uma linha por item, então no ListView temos que atribuir o valor True para a propriedade HasUnevenRows: 
  
  ````xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CalcFreelancer.ProjetosPage"
             Title="Projetos">
    <ContentPage.Content>
        <ListView ItemsSource="{Binding Projetos}"
                  IsPullToRefreshEnabled="True"
                  RefreshCommand="{Binding AtualizarDadosCommand}"
                  IsRefreshing="{Binding Atualizando}"
                  HasUnevenRows="True">
        </ListView>
    </ContentPage.Content>
</ContentPage>
````

Por fim vamos criar o layout dos itens da lista, para isso devemos criar um DataTemplate e dentro dele um ViewCell. Neste último criamos o nosso layout de exibição desejado. Como nossa lista é do tipo Projeto, dentro do ViewCell, ao utilizar o Binding conseguimos acessar cada item de nossa lista, ou seja, acessamos um objeto do tipo Projeto, podendo acessar as suas propriedades, como Nome e ValorTotal. Vamos criar um StackLayout e dentro dele duas Labels, uma para exibir o nome do projeto e outra o valor total do mesmo, utilizando do recurso do StringFormat para converter para moeda. Ficou assim:
  
  ````xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CalcFreelancer.ProjetosPage"
             Title="Projetos">
    <ContentPage.Content>
        <ListView ItemsSource="{Binding Projetos}"
                  IsPullToRefreshEnabled="True"
                   RefreshCommand="{Binding AtualizarDadosCommand}"
                   IsRefreshing="{Binding Atualizando}"
                  HasUnevenRows="True">
            <ListView.ItemTemplate>
                <DataTemplate>
                    <ViewCell>
                        <StackLayout Padding="10">
                            <Label Text="{Binding Nome, StringFormat='Nome {0}'}" 
                                   FontSize="Medium" />
                            <Label Text="{Binding ValorTotal,  StringFormat='Valor {0:C}'}"
                                   FontSize="Small"/>
                        </StackLayout>
                    </ViewCell>
                </DataTemplate>
            </ListView.ItemTemplate>
        </ListView>
    </ContentPage.Content>
</ContentPage>
````

### Adição da View ProjetosPage na nossa tab

Edite o arquivo HomePage.xaml e adicione a view ProjetosPage, ficando dessa forma:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<TabbedPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:CalcFreelancer"
             Title="Calculadora Freelancer"
             x:Class="CalcFreelancer.HomePage">
    
    <local:CalculoValorHoraPage/>
    <local:ProjetoPage/>
    <local:ProjetosPage/>

</TabbedPage>
````
### Item Selecionado no ListView

Vamos dar um Alert exibindo o nome do Projeto selecionado, apenas para aprendermos a obter o item selecionado na lista. 

Primeiramente na nossa ViewModel ProjetosPageViewModel vamos criar uma propriedade Bindable chamada Projeto:

```c#
...

private Projeto projeto;
public Projeto Projeto
{
    get { return projeto; }
    set { SetProperty(ref projeto, value); }
}
	
...
````

Toda vez que um projeto for escolhido na lista, o valor da propriedade Projeto será alterado, então vamos dar o nosso alerta dentro do set da nossa propriedade, mas antes devemos verificar se o projeto não é nulo, para evitar que ocorra erros:

```c#
...

private Projeto projeto;
public Projeto Projeto
{
	get { return projeto; }
	set
	{
		SetProperty(ref projeto, value);
		
		if(projeto != null)
			App.Current.MainPage.DisplayAlert("Projeto", projeto.Nome, "ok");
	}
}

...
````

Nossa ViewModel ficou assim:

```c#
public class ProjetosPageViewModel : ViewModelBase
{
	private readonly IProjetoService ProjetoService;

	public ObservableCollection<Projeto> Projetos { get; set; }

	private bool atualizando;
	public bool Atualizando
	{
	    get { return atualizando; }
	    set { SetProperty(ref atualizando, value); }
	}

	private Projeto projeto;
	public Projeto Projeto
	{
	    get { return projeto; }
	    set
	    {
		SetProperty(ref projeto, value);
		App.Current.MainPage.DisplayAlert("Projeto", projeto.Nome, "ok");
	    }
	}

	public Command AtualizarDadosCommand { get; }        

	public ProjetosPageViewModel(IProjetoService projetoService)
	{
	    ProjetoService = projetoService;
	    Projetos = new ObservableCollection<Projeto>();
	    ObterProjetos();
	    AtualizarDadosCommand = new Command(ExecuteAtualizarDadosCommand);
	}

	private async void ExecuteAtualizarDadosCommand()
	{
	    Atualizando = true;
	    await ObterProjetos();
	    Atualizando = false;
	}

	private async Task ObterProjetos()
	{
	    var projetos = await ProjetoService.ObterTodos();

	    if (Projetos.Count > 0)
	    {
		Projetos.Clear();
	    }

	    foreach (var projeto in projetos)
	    {
		Projetos.Add(projeto);
	    }
	}
}
````

Agora na nossa ProjetosPage.xaml, no ListView, atribua para a propriedade SelectedItem o Binding para nossa variável Projeto que criamos na nossa ViewModel, ficando assim:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CalcFreelancer.ProjetosPage"
             Title="Projetos">
    <ContentPage.Content>
        <ListView ItemsSource="{Binding Projetos}"
                  IsPullToRefreshEnabled="True"
                   RefreshCommand="{Binding AtualizarDadosCommand}"
                   IsRefreshing="{Binding Atualizando}"
                  HasUnevenRows="True"
                  SelectedItem="{Binding Projeto}">
            <ListView.ItemTemplate>
                <DataTemplate>
                    <ViewCell>
                        <StackLayout Padding="10">
                            <Label Text="{Binding Nome, StringFormat='Nome {0}'}" 
                                   FontSize="Medium" />
                            <Label Text="{Binding ValorTotal,  StringFormat='Valor {0:C}'}"
                                   FontSize="Small"/>
                        </StackLayout>
                    </ViewCell>
                </DataTemplate>
            </ListView.ItemTemplate>
        </ListView>
    </ContentPage.Content>
</ContentPage>
````

## Teste
Vá na guia de Projeto e cadastre um projeto, após vá na guia de Projetos e puxe para atualizar e veja o elemento cadastrado aparecendo na lista.

## Resultado

<img src="https://github.com/dayaneLima/CalculadoraFreelancer06/blob/master/Docs/Gifs/lista_funcionando.gif" alt="Lista funcionando" width="260">
