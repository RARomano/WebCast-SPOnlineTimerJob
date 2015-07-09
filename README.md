# SharePoint Online Timer Job hosted in Azure

Esse exemplo mostra como criar um TimerJob para o SharePoint Online utilizando SharePoint add-ins (anteriormente conhecido como apps) e armazenar no Azure.

## Rodando esse projeto

Para rodar esse exemplo você precisará:
- Visual Studio 2013
- SharePoint Online
- Azure Subscription

### 1 - Clonar ou fazer o download do Repositório

Rode o comando abaixo no Git Shell:

`git clone https://github.com/RARomano/WebCast-SPOnlineTimerJob.git`

### 2 - Registrar um novo App

Abrir a URL **"_layouts/AppRegNew.aspx"** no seu SharePoint 

Clicar no botão gerar do ID do Cliente e do Segredo do Cliente. Digitar o Título da App preencher um domínio para a APP (pode ser localhost) e uma URL de redirecionamento (pode ser a URL do seu SharePoint)

![Criar um App](http://rodrigoromano.net/wp-content/uploads/2015/03/4.jpg)

### 3 - Dar permissões ao Add-in

Abrir a URL **"_layouts/AppInv.aspx"** no seu tenant do SharePoint e digite o ClientID criado na etapa 2.

![Permissões](http://rodrigoromano.net/wp-content/uploads/2015/03/6.jpg)

No campo XML de Solicitação de Permissão do Aplicativo cole o XML abaixo:

```XML
<AppPermissionRequests AllowAppOnlyPolicy="true">
    <AppPermissionRequest Scope="http://sharepoint/content/sitecollection/web/list" Right="Manage" />
</AppPermissionRequests>
```

Com esse XML, você dará permissão em uma lista para o Add-In do SharePoint. Altere se for necessário.

### 4 - Alterar o arquivo App.Config

![App.Config](http://rodrigoromano.net/wp-content/uploads/2015/03/5.jpg)

### 5 - Alterar o Código e colocar a url do seu tenant/Site Collection

```C#
    [HttpGet]
		public string AddItem()
		{
			try
			{
				string url = "";
				using (var ctx = CreateClientContext(url))
				{
					var list = ctx.Web.Lists.GetByTitle("WebCast");

					var item = list.AddItem(new ListItemCreationInformation());
					item["Title"] = "WebCast_" + DateTime.Now.ToString("dd_MM_yyyy");
					item.Update();

					ctx.ExecuteQuery();
				}
				return "OK";
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}
```

### 6 - Publicar a aplicação no Azure

Clique com o botão direito na solution e clique em **Publish**.

![Publish](https://cloud.githubusercontent.com/assets/12012898/8604564/d56a754c-2655-11e5-83fd-b5c99ed90879.png)

Clique em **Microsoft Azure Web Apps**.

![Azure Web apps](https://cloud.githubusercontent.com/assets/12012898/8604588/ff76b72e-2655-11e5-9c36-64fec49dca26.png)

Clique em **New** e digite as informações necessárias, anote a **URL** que você criou para ser utilizada no próximo passo.



### 7 - Criar o Azure Job

Abra o seu portal de gerenciamento do azure [Azure Management Portal](https://manage.windowsazure.com/).

Clique em **Agendador**.

![Agendador](https://cloud.githubusercontent.com/assets/12012898/8604325/3e76b8f4-2654-11e5-807c-314fe01620c0.png)

Clique em **Criar trabalho do Agendador**.

![Agendador](https://cloud.githubusercontent.com/assets/12012898/8604397/c1a952f4-2654-11e5-99f9-d293664927c5.png)

Clique em **Criação Personalizada**.

![Criação](https://cloud.githubusercontent.com/assets/12012898/8604415/dd2fde3a-2654-11e5-9c4b-c30810bca922.png)

Dê um nome para a coleção de jobs.

![Job Collection](https://cloud.githubusercontent.com/assets/12012898/8604453/03bdc80a-2655-11e5-8672-f52e1042ba63.png)

Dê um nome para o job e coloque a url criada na etapa anterior.

![Job definition](https://cloud.githubusercontent.com/assets/12012898/8604495/4f7cdbd2-2655-11e5-86c0-da53b90a84ee.png)

Escolha a periodicidade

![Job definition](https://cloud.githubusercontent.com/assets/12012898/8604534/9b1e1cc2-2655-11e5-9180-570c9e964c97.png)







